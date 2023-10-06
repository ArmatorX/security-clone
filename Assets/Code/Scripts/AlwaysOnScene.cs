using UnityEngine;

public static class AlwaysOnScene
{
    public static Controller GameController {
        get {
            var gc = GameObject.Find("GameController");

            if (gc == null)
            {
                throw new System.Exception("An object tried to access the GameController, but GameController is not on the scene.");
            }
            
            return gc.GetComponent<Controller>();
        }
    }
}