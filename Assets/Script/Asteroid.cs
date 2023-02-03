using Assets.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private int rotationSpeed = 12;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private GameObject _explosionPrefab;
    private bool isDestroyed = false;
    private Player player;

    private SpawnManager spawnManager;

    void Start()
    {
        this.speed = CommonExtension.getRandomSpeed(10, 20);
        transform.position = new Vector3(CommonExtension.getRandomFloat(-10, 10), 15f, transform.position.z);
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            print("player is Null");
        }
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (player == null)
        {
            print("spawnManager is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        transform.Rotate(Vector3.forward* rotationSpeed);
        reposition();
       // transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    // Update is called once per frame

    private void reposition()
    {
        if (transform.position.y < 0)
        {
            //    transform.position = new Vector3(CommonExtension.getRandomFloat(-10,10), 20f, transform.position.z);
            //    life--;
            //}
            //if(this.life < 0)
            //{
            Destroy(this.gameObject);
        }
    }

    private void DestroyAstroid()
    {
        //m_Animator.SetTrigger("OnEnemyDeath");
        this.speed = 0;
        this.isDestroyed = true;
        var _explosion=Instantiate(_explosionPrefab, transform.position, transform.rotation, transform.parent);
        Destroy(_explosion, 2.5f);
        StartSpwnWave();
        Destroy(this.gameObject);
    }

    private void StartSpwnWave()
    {
        
        spawnManager.startSpawning();
    }

        private void OnTriggerEnter2D(Collider2D other)
    { 
        if (isDestroyed)
            return;
        if (other.tag.Equals("Player"))
        {
            var player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage(60);
            }
            player.incrementScore();
            this.DestroyAstroid();
        }
        if (other.tag.Equals("Sheild"))
        {
            player.incrementScore();
            this.DestroyAstroid();
        }
        if (other.tag.Equals("Laser"))
        {
             var laser = other.transform.GetComponent<Laser>();
            if (laser != null)
            {
                laser.DestroyLaser();
            }
            this.DestroyAstroid();
        }
    }
}
 