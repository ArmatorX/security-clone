using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class SecurityCameraClass
{
    private GameObject goSecurityCamera;
    private SecurityCamera securityCamera;
    private GameObject gameController;

    [SetUp]
    public void SetUp()
    {
        goSecurityCamera = new GameObject("SecurityCamera");
        securityCamera = goSecurityCamera.AddComponent<SecurityCamera>();
        securityCamera.ConeOfView = new GameObject("FakeCone");

        gameController = new GameObject("GameController");
        gameController.AddComponent<DummyController>();
    }

    [Test]
    public void OnSeenPlayer_CallLose() 
    {
        securityCamera.OnSeenPlayer();
        Assert.True(gameController.GetComponent<DummyController>().HasCalledLose);
    }

    [Test]
    public void Toggle_ConeOfViewActive_DisableConeOfView() 
    {
        securityCamera.ConeOfView.SetActive(true);
        securityCamera.Toggle();
        Assert.False(securityCamera.ConeOfView.activeSelf);
    }

    [Test]
    public void Toggle_ConeOfViewDisabled_KeepDisabled() 
    {
        securityCamera.ConeOfView.SetActive(false);
        securityCamera.Toggle();
        Assert.False(securityCamera.ConeOfView.activeSelf);
    }
}

[TestFixture]
public class SecurityCameraPrefab
{
    private GameObject securityCameraPrefab;
    private GameObject spyPrefab;
    private GameObject wallPrefab;
    private GameObject securityCamera;
    private GameObject gameController;

    [SetUp]
    public void SetUp()
    {
        securityCameraPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/LevelElements/Entities/SecurityCamera.prefab");
        spyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/Spy.prefab");
        wallPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/LevelElements/Wall.prefab");

        securityCamera = GameObject.Instantiate(securityCameraPrefab, Vector3.zero, Quaternion.identity);

        gameController = new GameObject("GameController");
        gameController.AddComponent<DummyController>();
    }

    [TearDown]
    public void TearDown()
    {
        foreach (GameObject o in GameObject.FindObjectsOfType<GameObject>())
        {
            GameObject.DestroyImmediate(o.gameObject);
        }
    }

    [UnityTest]
    public IEnumerator OnPlayerSeen_PlayerHitsConeOfView_Lose()
    {
        GameObject.Instantiate(spyPrefab, Vector3.right, Quaternion.identity);

        yield return new WaitForFixedUpdate();

        Assert.True(gameController.GetComponent<DummyController>().HasCalledLose);
    }

    [UnityTest]
    public IEnumerator OnPlayerSeen_WallHitsConeOfView_NotCallLose()
    {
        GameObject.Instantiate(wallPrefab, Vector3.right, Quaternion.identity);

        yield return new WaitForFixedUpdate();

        Assert.False(gameController.GetComponent<DummyController>().HasCalledLose);
    }

    [UnityTest]
    public IEnumerator OnToggle_PlayerHitsConeOfView_DontCallLose()
    {
        securityCamera.GetComponent<SecurityCamera>().Toggle();
        var player = GameObject.Instantiate(spyPrefab, Vector3.right, Quaternion.identity);

        yield return new WaitForFixedUpdate();

        Assert.False(gameController.GetComponent<DummyController>().HasCalledLose);
    }
}
