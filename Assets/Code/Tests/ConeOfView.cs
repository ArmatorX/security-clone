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

    [Test]
    public void ShouldGetParentEntity()
    {
        var entity = coneOfView.GetComponent<ConeOfView>().Entity;

        Assert.IsNotNull(entity);
        Assert.IsInstanceOf<EntityWithCoV>(entity);
        Assert.IsInstanceOf<MonoBehaviour>(entity);
    }

    [Test]
    public void ShouldCallOnSeenPlayerWhenPlayerCollidesWithIt()
    {
        var playerCollider = objectWithCollider.GetComponent<CircleCollider2D>();
        playerCollider.tag = "Player";

        coneOfView.GetComponent<ConeOfView>().OnTriggerEnter2D(playerCollider);

        Assert.IsTrue(entityWithCoV.GetComponent<DummyWithCoV>().HasCalledOnSeenPlayer);
    }

    [Test]
    public void ShouldNotCallOnSeenPlayerWhenOtherEntitiesCollideWithIt()
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
        foreach (GameObject o in GameObject.FindObjectsOfType<GameObject>())
        {
            GameObject.Destroy(o.gameObject);
        }
    }

    [Test]
    public void ShouldGetParentEntity()
    {
        var entity = coneOfView.GetComponent<ConeOfView>().Entity;

        Assert.IsNotNull(entity);
        Assert.IsInstanceOf<EntityWithCoV>(entity);
        Assert.IsInstanceOf<MonoBehaviour>(entity);
    }

    [UnityTest]
    public IEnumerator ShouldCallOnSeenPlayerWhenPlayerCollidesWithIt()
    {
        GameObject.Instantiate(spyPrefab, Vector3.zero, Quaternion.identity);

        yield return null;

        Assert.IsTrue(entityWithCoV.GetComponent<DummyWithCoV>().HasCalledOnSeenPlayer);
    }

    [UnityTest]
    public IEnumerator ShouldNotCallOnSeenPlayerWhenOtherEntitiesCollideWithIt()
    {
        GameObject.Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);

        yield return null;

        Assert.IsFalse(entityWithCoV.GetComponent<DummyWithCoV>().HasCalledOnSeenPlayer);
    }
}
