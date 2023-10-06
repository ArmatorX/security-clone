using UnityEngine;

public class SecurityCamera : MonoBehaviour, EntityWithCoV, Toggleable
{
    public GameObject ConeOfView;

    public void OnSeenPlayer()
    {
        GameObject.Find("GameController").GetComponent<GameController>().Lose();
    }

    public void Toggle()
    {
        ConeOfView.SetActive(false);
    }
}
