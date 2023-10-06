using UnityEngine;

public class Goal : MonoBehaviour
{
    private Controller _gameController;

    void Awake()
    {
        _gameController = AlwaysOnScene.GameController;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            _gameController.Win();
    }
}
