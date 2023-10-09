using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using System.Collections;

[TestFixture]
public class GoalPrefab
{
    private GameObject goalPrefab;
    private GameObject spyPrefab;
    private GameObject wallPrefab;

    public GameObject goal;
    public GameObject entity;
    public GameObject gameController;
    public DummyController dummyController;

    [SetUp]
    public void SetUp()
    {
        goalPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/LevelElements/Interactive/Goal.prefab");
        spyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/Spy.prefab");
        wallPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/LevelElements/Wall.prefab");

        goal = GameObject.Instantiate(goalPrefab, Vector3.zero, Quaternion.identity);

        gameController = new GameObject("GameController");
        dummyController = gameController.AddComponent<DummyController>();
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(goal);
        GameObject.DestroyImmediate(entity);
        GameObject.DestroyImmediate(gameController);
    }

    [UnityTest]
    public IEnumerator OnTriggerEnter2D_EntityPressedSwitch_ShouldNotCallWin()
    {
        entity = GameObject.Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);

        yield return new WaitForFixedUpdate();

        Assert.False(dummyController.HasCalledWin);
    }

    [UnityTest]
    public IEnumerator OnTriggerEnter2D_PlayerPressedSwitch_ShouldCallWin()
    {
        entity = GameObject.Instantiate(spyPrefab, Vector3.zero, Quaternion.identity);

        yield return new WaitForFixedUpdate();

        Assert.True(dummyController.HasCalledWin);
    }
}