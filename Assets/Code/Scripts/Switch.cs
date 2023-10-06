using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private GameController _gameController;
    public GameObject ToggleableObject;

    void Awake()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter2D()
    {
        ToggleableObject.GetComponent<Toggleable>().Toggle();
    }
}
