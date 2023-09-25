using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private bool _isStartingWaypoint = false;
    [SerializeField]
    private Enemy _parentEnemy;
    public Enemy ParentEnemy { get => _parentEnemy; set => _parentEnemy = value; }
    public bool IsStartingWaypoint { get => _isStartingWaypoint; set => _isStartingWaypoint = value; }
}
