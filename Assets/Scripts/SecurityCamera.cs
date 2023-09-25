using UnityEngine;

public class SecurityCamera : MonoBehaviour, EntityWithCoV
{
    public void OnSeenPlayer()
    {
        GameObject.Find("GameController").GetComponent<GameController>().Lose();
    }
}
