using UnityEngine;

public class SecurityCamera : MonoBehaviour, EntityWithCoV, Toggleable
{
    public GameObject ConeOfView;

    public void OnSeenPlayer()
    {
        AlwaysOnScene.GameController.Lose();
    }

    public void Toggle()
    {
        ConeOfView.SetActive(false);
    }
}
