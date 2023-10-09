using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class AlwaysOnSceneClass
{
    public GameObject gameController;
    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(gameController);
    }

    [Test]
    public void GameController_Instanced_GetController()
    {
        gameController = new GameObject("GameController");
        gameController.AddComponent<GameController>();
        var gc = AlwaysOnScene.GameController;

        Assert.IsInstanceOf<GameController>(gc);
    }

    [Test]
    public void GameController_InstancedDummy_GetController()
    {
        gameController = new GameObject("GameController");
        gameController.AddComponent<DummyController>();
        var gc = AlwaysOnScene.GameController;

        Assert.IsInstanceOf<DummyController>(gc);
    }

    [Test]
    public void GameController_NotIntanced_ThrowError()
    {
        Assert.Throws(Is.TypeOf<System.Exception>()
            .And.Message.EqualTo("An object tried to access the GameController, but GameController is not on the scene."),
            () => { var gc = AlwaysOnScene.GameController; });
    }
}