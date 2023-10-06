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

public class DummyWithCoVTest
{
    private GameObject dummyMock;

    [SetUp]
    public void SetUp()
    {
        dummyMock = new GameObject("Dummy");
        dummyMock.AddComponent<DummyWithCoV>();
    }

    [Test]
    public void ShouldNotChangePropertyWhenOnSeenPlayerHasNotBeenCalled()
    {
        var dummyWithCoV = dummyMock.GetComponent<DummyWithCoV>();
        Assert.IsFalse(dummyWithCoV.HasCalledOnSeenPlayer);
    }

    [Test]
    public void ShouldChangePropertyWhenOnSeenPlayerHasBeenCalled()
    {
        var dummyWithCoV = dummyMock.GetComponent<DummyWithCoV>();
        dummyWithCoV.OnSeenPlayer();
        Assert.IsTrue(dummyWithCoV.HasCalledOnSeenPlayer);
    }
}