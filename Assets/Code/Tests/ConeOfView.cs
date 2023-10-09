using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class ConeOfViewClass
{
    private GameObject coneOfView;
    private GameObject objectWithCollider;
    private GameObject entityWithCoV;

    [SetUp]
    public void SetUp()
    {
        entityWithCoV = new GameObject("EntityWithCoV");
        entityWithCoV.AddComponent<DummyWithCoV>();

        coneOfView = new GameObject("coneOfView");
        coneOfView.AddComponent<ConeOfView>();
        coneOfView.transform.parent = entityWithCoV.transform;

        objectWithCollider = new GameObject("ObjectWithCollider");
        objectWithCollider.AddComponent<CircleCollider2D>();
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(entityWithCoV);
        GameObject.DestroyImmediate(coneOfView);
        GameObject.DestroyImmediate(objectWithCollider);
    }

    [Test]
    public void Entity_FirstCall_NotNull()
    {
        var entity = coneOfView.GetComponent<ConeOfView>().Entity;

        Assert.IsNotNull(entity);
        Assert.IsInstanceOf<EntityWithCoV>(entity);
        Assert.IsInstanceOf<MonoBehaviour>(entity);
    }

    [Test]
    public void OnTriggerEnter2D_CollidesWithPlayer_CallOnSeenPlayer()
    {
        var playerCollider = objectWithCollider.GetComponent<CircleCollider2D>();
        playerCollider.tag = "Player";

        coneOfView.GetComponent<ConeOfView>().OnTriggerEnter2D(playerCollider);

        Assert.IsTrue(entityWithCoV.GetComponent<DummyWithCoV>().HasCalledOnSeenPlayer);
    }

    [Test]
    public void OnTriggerEnter2D_CollidesWithEntity_NotCallOnSeenPlayer()
    {
        var playerCollider = objectWithCollider.GetComponent<CircleCollider2D>();
        playerCollider.tag = "Entities";

        coneOfView.GetComponent<ConeOfView>().OnTriggerEnter2D(playerCollider);

        Assert.IsFalse(entityWithCoV.GetComponent<DummyWithCoV>().HasCalledOnSeenPlayer);
    }
}

[TestFixture]
public class ConeOfViewPrefab
{
    private GameObject spyPrefab;
    private GameObject coneOfViewPrefab;
    private GameObject wallPrefab;
    private GameObject coneOfView;
    private GameObject entityWithCoV;
    private GameObject entity;

    [SetUp]
    public void SetUp()
    {
        spyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/Spy.prefab");
        coneOfViewPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/ConeOfView.prefab");
        wallPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/LevelElements/Wall.prefab");

        entityWithCoV = new GameObject("EntityWithCoV");
        entityWithCoV.AddComponent<DummyWithCoV>();

        coneOfView = GameObject.Instantiate(coneOfViewPrefab, Vector3.zero, Quaternion.identity, entityWithCoV.transform);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(entityWithCoV);
        GameObject.DestroyImmediate(coneOfView);
        GameObject.DestroyImmediate(entity);
    }

    [Test]
    public void Entity_FirstCall_NotNull()
    {
        var entity = coneOfView.GetComponent<ConeOfView>().Entity;

        Assert.IsNotNull(entity);
        Assert.IsInstanceOf<EntityWithCoV>(entity);
        Assert.IsInstanceOf<MonoBehaviour>(entity);
    }

    [UnityTest]
    public IEnumerator OnTriggerEnter2D_CollidesWithPlayer_CallOnSeenPlayer()
    {
        entity = GameObject.Instantiate(spyPrefab, Vector3.zero, Quaternion.identity);

        yield return null;

        Assert.IsTrue(entityWithCoV.GetComponent<DummyWithCoV>().HasCalledOnSeenPlayer);
    }

    [UnityTest]
    public IEnumerator OnTriggerEnter2D_CollidesWithWall_NotCallOnSeenPlayer()
    {
        entity = GameObject.Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);

        yield return null;

        Assert.IsFalse(entityWithCoV.GetComponent<DummyWithCoV>().HasCalledOnSeenPlayer);
    }
}
