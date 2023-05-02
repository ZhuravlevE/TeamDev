using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayModeTests
{
    private PlayerMovement _player;
    private PhantomRed _enemyRed;
    private Key _key;
    private Objectives _objectives;
    private PhantomGreen _enemyGreen;
    private PhantomGreen _enemyYellow;


    [SetUp]
    public void Setup()
    {
        GameObject playerPrefab = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        GameObject enemyPrefab = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/PhantomRed"));
        GameObject keyPrefab = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Key"));
        GameObject greenPrefab = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/PhantomGreen"));
        GameObject yellowPrefab = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/PhantomYellow"));


        _player = playerPrefab.GetComponent<PlayerMovement>();
        _enemyRed = enemyPrefab.GetComponent<PhantomRed>();
        _key = keyPrefab.GetComponent<Key>();
        _objectives = playerPrefab.GetComponent<Objectives>();
        _enemyGreen = greenPrefab.GetComponent<PhantomGreen>();
        _enemyYellow = yellowPrefab.GetComponent<PhantomGreen>();

    }

    [TearDown]
    public void TearDown()
    {   
        Object.Destroy(_player.gameObject);
        Object.Destroy(_enemyRed.gameObject);
        Object.Destroy(_key.gameObject);
        Object.Destroy(_enemyGreen.gameObject);
        Object.Destroy(_enemyYellow.gameObject);
    }

    private void DestroyObjects(params GameObject[] objects)
    {
        foreach(var obj in objects)
            Object.Destroy(obj);
    }

    [Test]
    public void PlayModeTestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    [UnityTest]
    public IEnumerator PlayerDead()
    {
        GameObject player = MonoBehaviour.Instantiate<GameObject>(_player.gameObject, Vector3.zero, Quaternion.identity);
        GameObject enemy = MonoBehaviour.Instantiate<GameObject>(_enemyRed.gameObject, Vector3.zero, Quaternion.identity);

        yield return new WaitForSeconds(.1f);

        Assert.IsTrue(player.GetComponent<PlayerMovement>().isDead);

        DestroyObjects(player, enemy);
    }

    [UnityTest]
    public IEnumerator PlayerGetKeys()
    {
        GameObject player = MonoBehaviour.Instantiate<GameObject>(_player.gameObject, Vector3.zero, Quaternion.identity);

        int keys = player.GetComponent<Objectives>().KeyCount;

        GameObject key = MonoBehaviour.Instantiate<GameObject>(_key.gameObject, Vector3.zero, Quaternion.identity);

        yield return new WaitForSeconds(.1f);

        Assert.Less(player.GetComponent<Objectives>().KeyCount, keys);

        DestroyObjects(player, key);
    }

    [UnityTest]
    public IEnumerator RedPiksUpKey()
    {
        GameObject enemy = MonoBehaviour.Instantiate<GameObject>(_enemyRed.gameObject, Vector3.zero, Quaternion.identity);
        GameObject key = MonoBehaviour.Instantiate<GameObject>(_key.gameObject, Vector3.zero, Quaternion.identity);

        yield return new WaitForSeconds(.1f);

        Assert.IsTrue(key.GetComponent<Key>()._pick);

        DestroyObjects(enemy, key);
    }

    [UnityTest]
    public IEnumerator RedUpWithGreen()
    {
        GameObject red = MonoBehaviour.Instantiate<GameObject>(_enemyRed.gameObject, Vector3.zero, Quaternion.identity);

        float init_pos = red.transform.position.y;

        GameObject green = MonoBehaviour.Instantiate<GameObject>(_enemyGreen.gameObject, Vector3.zero, Quaternion.identity);

        yield return new WaitForSeconds(.1f);

        Assert.Greater(red.transform.position.y, init_pos);

        DestroyObjects(red, green);
    }

    [UnityTest]
    public IEnumerator RedDownWithYellow()
    {
        GameObject red = MonoBehaviour.Instantiate<GameObject>(_enemyRed.gameObject, Vector3.zero, Quaternion.identity);

        float init_pos = red.transform.position.y;

        GameObject yellow = MonoBehaviour.Instantiate<GameObject>(_enemyYellow.gameObject, Vector3.zero, Quaternion.identity);

        yield return new WaitForSeconds(.1f);

        Assert.Less(red.transform.position.y, init_pos);

        DestroyObjects(red, yellow);
    }
}
