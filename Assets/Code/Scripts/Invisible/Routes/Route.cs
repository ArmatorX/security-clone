using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Route: List<GameObject>
{
    public int position = 0;
    public Enemy enemy;
    public Route(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public bool IsValidRoute
    {
        get => Count >= 2;
    }

    public GameObject Current {
        get => this[position];
    }

    public abstract GameObject Next { get; }
}

public enum RouteTypes
{
    LOOP,
    BACK_AND_FORTH
}

[Serializable]
public class SerializableRoute
{
    public List<GameObject> waypoints;
    public RouteTypes type;
}