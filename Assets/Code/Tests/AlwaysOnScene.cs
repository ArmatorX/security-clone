using NUnit.Framework;
using System.Collections;
using UnityEngine;

[TestFixture]
public class AlwaysOnSceneClass
{
    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(GameObject.Find("GameController"));
    }

    [Test]
    public void GameController_Instanced_GetController()
    {
        new GameObject("GameController").AddComponent<GameController>();
        var gc = AlwaysOnScene.GameController;

        Assert.IsInstanceOf<GameController>(gc);
    }

    [Test]
    public void GameController_InstancedDummy_GetController()
    {
        new GameObject("GameController").AddComponent<DummyController>();
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