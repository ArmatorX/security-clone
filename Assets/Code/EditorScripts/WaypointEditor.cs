using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    private SerializedObject _soWaypoint;
    private Waypoint _waypoint;

    private Waypoint waypoint
    {
        get
        {
            if (_waypoint == null)
            {
                _waypoint = (Waypoint)target;
            }

            return _waypoint;
        }
    }

    public void OnSceneGUI()
    {
        EnemyEditor.DrawRoute(waypoint.ParentEnemy);
        UpdateParentEnemyPosition();
    }

    private void UpdateParentEnemyPosition()
    {
        if (!EditorApplication.isPlaying && waypoint.IsStartingWaypoint)
            waypoint.ParentEnemy.transform.position = waypoint.transform.position;
    }

    public static GameObject InstantiateWaypoint(Vector3 position, Enemy parent) {
        var level = GameObject.Find("Level").transform;
        var prefab = parent.waypointPrefab;
        var waypoint = Instantiate(prefab, position, Quaternion.identity, level);
        var soWaypoint = new SerializedObject(waypoint.GetComponent<Waypoint>());
        soWaypoint.FindProperty("_parentEnemy").objectReferenceValue = parent;
        soWaypoint.FindProperty("_isStartingWaypoint").boolValue = position == parent.transform.position;
        soWaypoint.ApplyModifiedProperties();
        return waypoint;
    }
}
