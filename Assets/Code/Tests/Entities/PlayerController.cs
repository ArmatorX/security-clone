using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class PlayerControllerPrefab
{
    private GameObject spyPrefab;
    private GameObject player;

    [SetUp]
    public void SetUp()
    {
        spyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/Spy.prefab");

        player = GameObject.Instantiate(spyPrefab, Vector3.zero, Quaternion.identity);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(player);
    }

    [UnityTest]
    public IEnumerator Update_NoPlayerInput_ShouldNotMove()
    {

        yield return null;
    }

    [UnityTest]
    public IEnumerator Update_PlayerInputUp_ShouldMoveUp()
    {

        yield return null;
    }

    [UnityTest]
    public IEnumerator Update_PlayerInputDiagonally_ShouldMoveDiagonally()
    {

        yield return null;
    }
}
