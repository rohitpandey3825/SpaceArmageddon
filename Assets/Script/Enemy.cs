using Assets.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;
    private int life = 5;
    private Player player;
    [SerializeField]
    private GameObject _laserPrefab;
    // Start is called before the first frame update
    void Start()
    {
        this.speed = CommonExtension.getRandomSpeed(10, 20);
        transform.position = new Vector3(CommonExtension.getRandomFloat(-10, 10), 20f, transform.position.z);
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            print("player is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        reposition();
    }
    private void reposition()
    {
        transform.Translate(Vector3.down * this.speed);
    
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(CommonExtension.getRandomFloat(-10,10), 20f, transform.position.z);
            life--;
        }
        if(this.life < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Player"))
        {
            var player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage(20);
            }
            player.incrementScore();  
            Destroy(this.gameObject);
        }
        if (other.tag.Equals("Sheild"))
        {
            player.incrementScore();
            Destroy(this.gameObject);
        }
        if (other.tag.Equals("Laser"))
        {
            var laser = other.transform.GetComponent<Laser>();
            if (laser != null)
            {
                laser.DestroyLaser();
            }
            player.incrementScore();
            Destroy(this.gameObject);
        }
    }
}
