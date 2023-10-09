using UnityEngine;
using NUnit.Framework;

public class DummyToggle : MonoBehaviour, Toggleable
{
    private bool _hasCalledToggle = false;
    public bool HasCalledToggle { get => _hasCalledToggle; }

    public void Toggle()
    {
        _hasCalledToggle = true;
        return;
    }
}

[TestFixture]
public class DummyToggleTest
{
    private GameObject dummyMock;

    [SetUp]
    public void SetUp()
    {
        dummyMock = new GameObject("Dummy");
        dummyMock.AddComponent<DummyToggle>();
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(dummyMock);
    }

    [Test]
    public void Toggle_NotCalled_HasCalledToggleFalse()
    {
        var dummyWithCoV = dummyMock.GetComponent<DummyToggle>();
        Assert.IsFalse(dummyWithCoV.HasCalledToggle);
    }

    [Test]
    public void Toggle_Called_HasCalledToggleTrue()
    {
        var dummyWithCoV = dummyMock.GetComponent<DummyToggle>();
        dummyWithCoV.Toggle();
        Assert.IsTrue(dummyWithCoV.HasCalledToggle);
    }
}