using UnityEngine;
using System.Collections;
using Assets.Common;
using System.Threading.Tasks;

public class SpawnManager : MonoBehaviour
{
    private bool spawn = true;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private PowerUps _powerUpsPrefabs;
    [SerializeField]
    private GameObject _EnemyContainer;

    IEnumerator WaitAndCreate(float WaitTime)
    {
        // suspend execution for WaitTime seconds
        while (true)
        {
            if (this.spawn)
            {
                Instantiate(_enemyPrefab, _EnemyContainer.transform);
                yield return new WaitForSeconds(WaitTime);
            }
        }
    }

    IEnumerator WaitAndCreatePowerUp(float StartTime, float WaitTime)
    {
        yield return new WaitForSeconds(StartTime);
        while (true)
        {
            var powerupPrefab = _powerUpsPrefabs.getRandomPowerUp();
            if (this.spawn)
            {
                Instantiate(powerupPrefab, _EnemyContainer.transform);
                yield return new WaitForSeconds(WaitTime);
            }
        }
    }

    void Update()
    {
        createSpawn();
        DestroySubroutines();
    }

    private void createSpawn()
    {
        var input = Input.GetKeyDown(KeyCode.Tab);
        if (input)
        {
            var time = CommonExtension.getRandomFloat(5, 10);
            StartCoroutine(WaitAndCreate(time));
            time = CommonExtension.getRandomFloat(15, 30);
            StartCoroutine(WaitAndCreatePowerUp(10,time));
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

    public void DestroyAllSubroutines()
    {
        this.StopAllCoroutines();
        this.stopSpawn();
        print("All WaitAndCreate Ended" + Time.time);
    }

    private void stopSpawn()
    {      
        this.spawn = !this.spawn;
    }
}
