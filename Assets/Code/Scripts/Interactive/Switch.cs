using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject ToggleableObject;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            ToggleableObject.GetComponent<Toggleable>().Toggle();
        }
    }
}
