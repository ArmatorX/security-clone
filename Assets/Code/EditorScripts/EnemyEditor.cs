using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    private SerializedObject _soEnemy;
    private Enemy _enemy;
    private Enemy enemy
    {
        get
        {
            if (_enemy == null)
            {
                _enemy = (Enemy)target;
            }

            return _enemy;
        }
    }
    private SerializedObject soEnemy
    {
        get
        {
            if (_soEnemy == null)
            {
                _soEnemy = new SerializedObject(enemy);
            }

            return serializedObject;
        }
    }

    public void OnSceneGUI()
    { 
        DrawRoute(enemy);
        UpdateStartingWaypointPosition();
    }

    private void UpdateStartingWaypointPosition()
    {
        if (!EditorApplication.isPlaying && enemy.Route.Count >= 1 && enemy.Route[0] != null)
        {
            var startingWaypoint = enemy.Route[0];
            startingWaypoint.transform.position = enemy.transform.position;
        }
    }

    public static void DrawRoute(Enemy enemy)
    {
        if (enemy.HasValidRoute)
        {
            for (int i = 1; i < enemy.Route.Count; i++)
            {
                DrawDottedLine(enemy, i);
            }

            if (enemy.Route.Count > 2 && enemy.Route is LoopRoute)
            {
                DrawDottedLine(enemy, 0, enemy.Route.Count - 1);
            }
        }
    }

    private static void DrawDottedLine(Enemy enemy, int startingWaypointIndex, int finishWaypointIndex = -1)
    {
        if (finishWaypointIndex == -1) finishWaypointIndex = startingWaypointIndex - 1;

        var p2 = enemy.Route[finishWaypointIndex].transform.position;
        var p1 = enemy.Route[startingWaypointIndex].transform.position;
        Handles.DrawDottedLine(p1, p2, 5);
    }

    public override void OnInspectorGUI()
    {
        // Update pending changes in SerializedObject
        soEnemy.UpdateIfRequiredOrScript();

        DrawDefaultInspector();

        var buttonText = enemy.Route.Count == 0 ? "Create new route" : "Add waypoint to route";

        if (GUILayout.Button(buttonText))
        {
            AddWaypointToRoute();
        }

        if (enemy.Route.Count > 0)
        {
            if (GUILayout.Button("Clear route"))
            {
                ClearRoute();
            }
        }
    }

    private GameObject InstantiateWaypoint(bool isStartingWaypoint = false)
    {
        var position = isStartingWaypoint ? enemy.transform.position : Vector3.zero;
        return WaypointEditor.InstantiateWaypoint(position, enemy);
    }

    private void ClearRoute()
    {
        foreach (GameObject waypoint in enemy.Route)
        {
            DestroyImmediate(waypoint);
        }

        soEnemy.FindProperty("serializableRoute.waypoints").ClearArray();

        soEnemy.ApplyModifiedProperties();
    }

    private void AddWaypointToRoute()
    {
        var index = serializedObject.FindProperty("serializableRoute.waypoints").arraySize;
        serializedObject.FindProperty("serializableRoute.waypoints").InsertArrayElementAtIndex(index);
        serializedObject.FindProperty("serializableRoute.waypoints.Array.data[" + index + "]").objectReferenceValue = InstantiateWaypoint(index == 0);
        serializedObject.ApplyModifiedProperties();
    }
}