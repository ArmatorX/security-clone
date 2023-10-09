using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using System.Collections;

[TestFixture]
public class SwitchClass
{
    private GameObject goSwitch;
    private GameObject goToggleable;
    private GameObject objectWithCollider;
    private Switch sswitch;

    [SetUp]
    public void SetUp()
    {
        goSwitch = new GameObject("Switch");
        sswitch = goSwitch.AddComponent<Switch>();

        goToggleable = new GameObject("Toggleable");
        goToggleable.AddComponent<DummyToggle>();
        sswitch.ToggleableObject = goToggleable;

        objectWithCollider = new GameObject("ObjectWithCollider");
        objectWithCollider.AddComponent<CircleCollider2D>();
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(goSwitch);
        GameObject.DestroyImmediate(goToggleable);
        GameObject.DestroyImmediate(objectWithCollider);
    }

    [Test]
    public void OnTriggerEnter2D_PlayerPressedSwitch_CallToggle()
    {
        var playerCollider = objectWithCollider.GetComponent<CircleCollider2D>();
        playerCollider.tag = "Player";

        sswitch.OnTriggerEnter2D(playerCollider);

        Assert.True(goToggleable.GetComponent<DummyToggle>().HasCalledToggle);
    }


    [Test]
    public void OnTriggerEnter2D_EnemyPressedSwitch_NotCallToggle()
    {
        var enemyCollider = objectWithCollider.GetComponent<CircleCollider2D>();

        sswitch.OnTriggerEnter2D(enemyCollider);

        Assert.False(goToggleable.GetComponent<DummyToggle>().HasCalledToggle);
    }
}

[TestFixture]
public class SwitchPrefab
{
    private GameObject switchPrefab;
    private GameObject spyPrefab;
    private GameObject wallPrefab;
    private GameObject securityCameraPrefab;

    public GameObject sswitch;
    public GameObject securityCamera;
    public GameObject player;

    [SetUp]
    public void SetUp()
    {
        switchPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/LevelElements/Interactive/Computer.prefab");
        securityCameraPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/LevelElements/Entities/SecurityCamera.prefab");
        spyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/Spy.prefab");
        wallPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/LevelElements/Wall.prefab");

        sswitch = GameObject.Instantiate(switchPrefab, new Vector3(10, 10), Quaternion.identity);
        securityCamera = GameObject.Instantiate(securityCameraPrefab, Vector3.zero, Quaternion.identity);
        sswitch.GetComponent<Switch>().ToggleableObject = securityCamera;
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(sswitch);
        GameObject.DestroyImmediate(securityCamera);
        GameObject.DestroyImmediate(player);
    }

    [UnityTest]
    public IEnumerator OnTriggerEnter2D_PlayerPressedSwitch_DisableCameraCoV()
    {
        player = GameObject.Instantiate(spyPrefab, new Vector3(10, 10), Quaternion.identity);
        
        yield return new WaitForFixedUpdate();

        Assert.False(securityCamera.GetComponent<SecurityCamera>().ConeOfView.activeSelf);
    }

    [UnityTest]
    public IEnumerator OnTriggerEnter2D_EntityPressedSwitch_CameraCoVUnaltered()
    {
        player = GameObject.Instantiate(wallPrefab, new Vector3(10, 10), Quaternion.identity);

        yield return new WaitForFixedUpdate();

        Assert.True(securityCamera.GetComponent<SecurityCamera>().ConeOfView.activeSelf);
    }
}