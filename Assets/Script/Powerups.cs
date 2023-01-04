using Assets.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    private float speed;
    private int life = 2;

    [SerializeField]
    private PowerUp powerup;
    [SerializeField]
    private GameObject _laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.speed = 15 * Time.deltaTime;
        transform.position = new Vector3(CommonExtension.getRandomFloat(-10, 10), 20f, transform.position.z);
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
            transform.position = new Vector3(CommonExtension.getRandomFloat(-10, 10), 20f, transform.position.z);
            life--;
        }
        if (this.life < 0)
        {
            Destroy(this.gameObject);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("tag==" + other.tag);

        if (other.tag.Equals("Player"))
        {
            var player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.EnablePowerUp(this.powerup);
            }   
            else
            {
                print("player is null");
            }
            Destroy(this.gameObject);
        }

        //if (other.tag.Equals("Laser"))
        //{
        //    Destroy(other.gameObject);
        //    Destroy(this.gameObject);
        //}
    }
}
