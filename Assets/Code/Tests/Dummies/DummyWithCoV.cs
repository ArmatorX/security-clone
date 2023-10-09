using UnityEngine;
using NUnit.Framework;

public class DummyWithCoV : MonoBehaviour, EntityWithCoV
{
    private bool _hasCalledOnSeenPlayer = false;
    public bool HasCalledOnSeenPlayer { get => _hasCalledOnSeenPlayer; }

    public void OnSeenPlayer()
    {
        _hasCalledOnSeenPlayer = true;
        return;
    }
}

[TestFixture]
public class DummyWithCoVTest
{
    private GameObject dummyMock;

    [SetUp]
    public void SetUp()
    {
        dummyMock = new GameObject("Dummy");
        dummyMock.AddComponent<DummyWithCoV>();
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(dummyMock);
    }

    [Test]
    public void OnSeenPlayer_NotCalled_HasCalledOnSeenPlayerFalse()
    {
        var dummyWithCoV = dummyMock.GetComponent<DummyWithCoV>();
        Assert.IsFalse(dummyWithCoV.HasCalledOnSeenPlayer);
    }

    [Test]
    public void OnSeenPlayer_Called_HasCalledOnSeenPlayerTrue()
    {
        var dummyWithCoV = dummyMock.GetComponent<DummyWithCoV>();
        dummyWithCoV.OnSeenPlayer();
        Assert.IsTrue(dummyWithCoV.HasCalledOnSeenPlayer);
    }
}