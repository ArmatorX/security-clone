using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

public class DummyController : MonoBehaviour, Controller
{
    private bool _hasCalledLose = false;
    private bool _hasCalledStart = false;
    private bool _hasCalledStartLevel = false;
    private bool _hasCalledWin = false;

    public bool HasCalledLose { get => _hasCalledLose; }
    public bool HasCalledStart { get => _hasCalledStart; }
    public bool HasCalledStartLevel { get => _hasCalledStartLevel; }
    public bool HasCalledWin { get => _hasCalledWin; }

    public void Lose()
    {
        _hasCalledLose = true;
        return;
    }

    public void Start()
    {
        _hasCalledStart = true;
        return;
    }

    public void StartLevel()
    {
        _hasCalledStartLevel = true;
        return;
    }

    public void Win()
    {
        _hasCalledWin = true;
        return;
    }
}

[TestFixture]
public class DummyControllerTest
{
    private GameObject controllerMock;

    [SetUp]
    public void SetUp()
    {
        controllerMock = new GameObject("Controller");
        controllerMock.AddComponent<DummyController>();
    }

    [Test]
    public void DummyController_Instanced_HasCalledAllFalse()
    {
        var dummyController = controllerMock.GetComponent<DummyController>();
        Assert.IsFalse(dummyController.HasCalledLose);
        Assert.IsFalse(dummyController.HasCalledStart);
        Assert.IsFalse(dummyController.HasCalledStartLevel);
        Assert.IsFalse(dummyController.HasCalledWin);
    }

    [Test]
    public void Lose_Called_HasCalledLoseTrue()
    {
        var dummyController = controllerMock.GetComponent<DummyController>();
        dummyController.Lose();
        Assert.IsTrue(dummyController.HasCalledLose);
    }

    [Test]
    public void Start_Called_HasCalledLoseTrue()
    {
        var dummyController = controllerMock.GetComponent<DummyController>();
        dummyController.Start();
        Assert.IsTrue(dummyController.HasCalledStart);
    }

    [Test]
    public void StartLevel_Called_HasCalledLoseTrue()
    {
        var dummyController = controllerMock.GetComponent<DummyController>();
        dummyController.StartLevel();
        Assert.IsTrue(dummyController.HasCalledStartLevel);
    }

    [Test]
    public void Win_Called_HasCalledLoseTrue()
    {
        var dummyController = controllerMock.GetComponent<DummyController>();
        dummyController.Win();
        Assert.IsTrue(dummyController.HasCalledWin);
    }
}
