using UnityEngine;

public class Goal : MonoBehaviour
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
