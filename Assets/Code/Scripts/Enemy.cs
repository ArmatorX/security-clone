using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, EntityWithCoV
{
    public List<GameObject> route;
    public GameObject waypointPrefab;
    public bool HasValidRoute
    {
        get => route != null && route.Count >= 2;
    }
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float stayAtWaypointForSeconds;
    [SerializeField]
    private EnemyState state = EnemyState.PATROLLING;

    private Vector3 nextWaypoint;
    private int indexNextWaypoint = 1;
    private float timeIdle = 0f;

    public void Awake()
    {
        if (route.Count <= 1)
        {
            InvalidRoute();
            return;
        }

        UpdateWaypoint();
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
        Debug.LogError("Invalid enemy route. The following enemy has only " + route.Count + " waypoints on its route.", gameObject);
    }

    private void Patrol()
    {
        float step = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, nextWaypoint) < step)
        {
            transform.position = nextWaypoint;
            SelectNextWaypoint();
            state = EnemyState.IDLE;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint, step);
    }

    private void Rotate()
    {
        float step = rotationSpeed * Time.deltaTime;
        var direction = nextWaypoint - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if ((Mathf.Abs(angle - transform.eulerAngles.z) < step) || (Mathf.Abs(angle - transform.eulerAngles.z) > 360 - step))
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
            state = EnemyState.PATROLLING;
            return;
        }

        transform.eulerAngles += new Vector3(0, 0, step);
    }

    private void UpdateWaypoint()
    {
        nextWaypoint = route[indexNextWaypoint].transform.position;
    }

    private void SelectNextWaypoint()
    {
        indexNextWaypoint++;
        if (indexNextWaypoint == route.Count) indexNextWaypoint = 0;
        UpdateWaypoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            OnSeenPlayer();
    }
    public void OnSeenPlayer()
    {
        GameObject.Find("GameController").GetComponent<GameController>().Lose();
    }
}

enum EnemyState
{
    PATROLLING,
    IDLE,
    ROTATING
}