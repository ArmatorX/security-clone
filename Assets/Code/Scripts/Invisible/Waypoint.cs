using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Enemy parentEnemy;
    public bool IsStartingWaypoint {
        get => parentEnemy.Route[0] == this;
    }
}
