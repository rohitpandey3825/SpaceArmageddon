using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed;
    private float fireRate = .5f;
    private float nextFire = 0.0f;

    [SerializeField]
    private int health = 100;
    [SerializeField]
    private int lifes = 3;
    [SerializeField]
    private GameObject _laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.speed = CommonExtension.getRandomSpeed(20,30);
        transform.position = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        FireLaser();
        CalculateMovement();
    }

    private void FireLaser()
    {
        var input = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0); 
        if ( input && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
          //  if (count <= 6)
          //  {
          //      this.count++;
                Instantiate(_laserPrefab, transform.position + new Vector3(0, .8f, 0), Quaternion.identity);
         //   }
        }

    }
    private void CalculateMovement()
    {
        var dirction = getPositionVector();
        transform.Translate(dirction * this.speed);
        reposition();
    }

    private void reposition()
    {
        var newPositon = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        bool isReposition = false;
        if (transform.position.x > 10)
        {
            newPositon.x = -10;
            isReposition = true;
        }
        else if (transform.position.x < -10)
        {
            newPositon.x = 10;
            isReposition = true;
        }
        if (transform.position.y >= 10)
        {
            newPositon.y = 10;
            isReposition = true;
        }
        else if (transform.position.y <= 0)
        {
            newPositon.y = 0;
            isReposition = true;
        }
        if (Mathf.Abs(transform.position.z) > 10)
        {
            newPositon.z = -transform.position.z;
            isReposition = true;
        }

        if(isReposition) 
            transform.position = newPositon;
    }
    private Vector3 getPositionVector()
    {
        var HorizontalInput = Input.GetAxis("Horizontal");
        var VerticalInput = Input.GetAxis("Vertical");
        return Vector3.right * HorizontalInput + Vector3.up * VerticalInput;
    }

    public void Damage(int value)
    {
        this.health = health - value;
        if(health < 0)
        {
            this.health = 100;
            lifes--;
        }
        if(lifes < 0)
        {
            Destroy(this.gameObject);
        }
    }

}
