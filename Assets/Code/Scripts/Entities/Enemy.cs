using log4net.Core;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour, EntityWithCoV, ISerializationCallbackReceiver
{
    private Route _route;
    public GameObject waypointPrefab;
    public SerializableRoute serializableRoute;
    public bool HasValidRoute
    {
        get => Route != null && Route.IsValidRoute;
    }
    public Route Route { get => _route; set => _route = value; }

    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float stayAtWaypointForSeconds;
    [SerializeField]
    private EnemyState state = EnemyState.PATROLLING;

    private Vector3 targetPosition;
    private float timeIdle = 0f;

    public void Awake()
    {
        if (!Route.IsValidRoute)
        {
            InvalidRoute();
            return;
        }

        targetPosition = Route.Next.transform.position;
    }

    public void Update()
    {
        switch(state)
        {
            case EnemyState.PATROLLING:
                Patrol();
                break;
            case EnemyState.ROTATING:
                Rotate();
                break;
            case EnemyState.IDLE:
                timeIdle += Time.deltaTime;
                if (timeIdle >= stayAtWaypointForSeconds)
                {
                    state = EnemyState.ROTATING;
                    timeIdle = 0f;
                }
                break;
        }
    }

    private void InvalidRoute()
    {
        gameObject.SetActive(false);
        Debug.LogError("Invalid enemy route. The following enemy has only " + Route.Count + " waypoints on its route.", gameObject);
    }

    private void Patrol()
    {
        float step = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) < step)
        {
            transform.position = targetPosition; // Correct for error
            targetPosition = Route.Next.transform.position; // Set next waypoint
            state = EnemyState.IDLE;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    private void Rotate()
    {
        float step = rotationSpeed * Time.deltaTime;
        var direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if ((Mathf.Abs(angle - transform.eulerAngles.z) < step) || (Mathf.Abs(angle - transform.eulerAngles.z) > 360 - step))
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
            state = EnemyState.PATROLLING;
            return;
        }

        transform.eulerAngles += new Vector3(0, 0, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            OnSeenPlayer();
    }
    public void OnSeenPlayer()
    {
        AlwaysOnScene.GameController.Lose();
    }

    public void OnBeforeSerialize()
    {
        if (Route == null)
        {
            return;
        }

        serializableRoute = new SerializableRoute();
        if (Route is LoopRoute)
        {
            serializableRoute.type = RouteTypes.LOOP;
        }
        else if (Route is BackAndForthRoute)
        {
            serializableRoute.type = RouteTypes.BACK_AND_FORTH;
        }
        serializableRoute.waypoints = new List<GameObject>(Route);
    }

    public void OnAfterDeserialize()
    {
        if (serializableRoute == null)
        {
            return;
        }

        switch (serializableRoute.type)
        {
            case RouteTypes.LOOP:
                Route = new LoopRoute(this);
                break;
            case RouteTypes.BACK_AND_FORTH:
                Route = new BackAndForthRoute(this);
                break;
        }

        Route.AddRange(serializableRoute.waypoints);
    }
}

enum EnemyState
{
    PATROLLING,
    IDLE,
    ROTATING
}