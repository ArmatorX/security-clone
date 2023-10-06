using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class ConeOfViewClass
{
    private GameObject coneOfViewMock;
    private GameObject objectWithColliderMock;
    private GameObject entityWithCoVMock;

    [SetUp]
    public void SetUp()
    {
        entityWithCoVMock = new GameObject("EntityWithCoV");
        entityWithCoVMock.AddComponent<DummyWithCoV>();

        coneOfViewMock = new GameObject("coneOfView");
        coneOfViewMock.AddComponent<ConeOfView>();
        coneOfViewMock.transform.parent = entityWithCoVMock.transform;

        objectWithColliderMock = new GameObject("ObjectWithCollider");
        objectWithColliderMock.AddComponent<CircleCollider2D>();
    }

    [Test]
    public void ShouldGetParentEntity()
    {
        var entity = coneOfViewMock.GetComponent<ConeOfView>().Entity;

        Assert.IsNotNull(entity);
        Assert.IsInstanceOf<EntityWithCoV>(entity);
        Assert.IsInstanceOf<MonoBehaviour>(entity);
    }

    [Test]
    public void ShouldCallOnSeenPlayerWhenPlayerCollidesWithIt()
    {
        var playerCollider = objectWithColliderMock.GetComponent<CircleCollider2D>();
        playerCollider.tag = "Player";

        coneOfViewMock.GetComponent<ConeOfView>().OnTriggerEnter2D(playerCollider);

        Assert.IsTrue(entityWithCoVMock.GetComponent<DummyWithCoV>().HasCalledOnSeenPlayer);
    }

    [Test]
    public void ShouldNotCallOnSeenPlayerWhenOtherEntitiesCollideWithIt()
    {
        var playerCollider = objectWithColliderMock.GetComponent<CircleCollider2D>();
        playerCollider.tag = "Entities";

        coneOfViewMock.GetComponent<ConeOfView>().OnTriggerEnter2D(playerCollider);

        Assert.IsFalse(entityWithCoVMock.GetComponent<DummyWithCoV>().HasCalledOnSeenPlayer);
    }
}

public class ConeOfViewPrefab
{
    private GameObject coneOfView;

    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public void ShouldCallOnSeenPlayerWhenPlayerCollidesWithIt()
    {

    }
}
