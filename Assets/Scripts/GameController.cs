using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject _player;
    private int _currentLevel = 0;
    private GameObject _level;
    public List<GameObject> levels;
    public GameObject playerPrefab;
    public bool startInDebugMode;

    void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        if (startInDebugMode)
        {
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            _level = allObjects[allObjects.Length - 3];
            Debug.Log(_level.name);
        }
        else
        {
            _level = Instantiate(levels[_currentLevel]);
        }
        CreatePlayerOnStart();
    }

    private void CreatePlayerOnStart()
    {
        var playerStart = _level.transform.GetChild(0).Find("PlayerStart");
        _player = Instantiate(playerPrefab, playerStart.transform.position, Quaternion.identity);
        playerStart.gameObject.SetActive(false);
    }

    public void Win()
    {
        Destroy(_player);
        Destroy(_level);
        _currentLevel++;
        StartLevel();
    }

    public void Lose()
    {
        Destroy(_player);
        CreatePlayerOnStart();
    }
}
