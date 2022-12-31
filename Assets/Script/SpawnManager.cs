using UnityEngine;
using System.Collections;
using Common;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    private bool spawn = false;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _EnemyContainer;
    IEnumerator WaitAndCreate(float WaitTime)
    {
        // suspend execution for 5 seconds
        while (true)
        {
            if (this.spawn)
            {
               Instantiate(_enemyPrefab, _EnemyContainer.transform);
            }
            yield return new WaitForSeconds(WaitTime);
        }
    }

    void Update()
    {
        createSpawn();
        stopSpawn();
        DestroySubroutines();
    }

    private void createSpawn()
    {
        var input = Input.GetKeyDown(KeyCode.Tab);
        if (input)
        {
            var time = CommonExtension.getRandomFloat(5, 10);
            print($"Spawner created at {Time.time} which will repete every {time} seconds");
            StartCoroutine(WaitAndCreate(time));
            print("Spawner Ended" + Time.time);
        }
    }
    private void DestroySubroutines()
    {
        var input = Input.GetKeyDown(KeyCode.Escape);
        if (input)
        {
            StopAllCoroutines();
            print("All WaitAndCreate Ended" + Time.time);
        }
    }
    private void stopSpawn()
    {
        var input = Input.GetKeyDown(KeyCode.LeftControl);
        if (input)
        {
            this.spawn = !this.spawn;
        }
    }
}
