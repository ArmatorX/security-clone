using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private GameController _gameController;

    void Awake()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter2D()
    {
        _gameController.Win();
    }
}
