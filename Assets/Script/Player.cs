using Assets.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed;
    private float fireRate = .5f ;
    private float nextFire = 0 ;
    private bool isTripleShotEnabled = false;

    [SerializeField]
    private int health = 100;
    [SerializeField]
    private int lifes = 3;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private GameObject _thrusterPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private GameObject _laserContainer;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        this.speed = 15 * Time.deltaTime;
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            print("spawn maager is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        FireLaser();
        CalculateMovement();
        
    }

    private void FireLaser()
    {
         var initPrefab = isTripleShotEnabled ? _tripleLaserPrefab : _laserPrefab;
        var input = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0);
        if (input && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //  if (count <= 6)
            //  {
            //      this.count++;
            Instantiate(initPrefab, transform.position + new Vector3(0, .8f, 0), Quaternion.identity, _laserContainer.transform);
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
        if (transform.position.x > 20)
        {
            newPositon.x = -20;
            isReposition = true;
        }
        else if (transform.position.x < -20)
        {
            newPositon.x = 20;
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

        if (isReposition)
            transform.position = newPositon;
    }
    private Vector3 getPositionVector()
    {
        var HorizontalInput = Input.GetAxis("Horizontal");
        var VerticalInput = Input.GetAxis("Vertical");
        return Vector3.right * HorizontalInput + Vector3.up * VerticalInput;
    }

    public void EnablePowerUp(PowerUp powerUp)
    {
        switch (powerUp)
        {
            case PowerUp.Sheild:
                 StartCoroutine(this.EnableThenDisableSheild());
                break;
            case PowerUp.Speed:
                StartCoroutine(this.EnableThenDoubleSpeed());
                break;
            case PowerUp.tripleShot:
                StartCoroutine(this.EnableThenDisableTripleShot());
                break;
        }
    }

    private IEnumerator EnableThenDisableTripleShot()
    {
        this.isTripleShotEnabled = true;
        yield return new WaitForSeconds(10);
        this.isTripleShotEnabled = false;
    }
    
    private IEnumerator EnableThenDisableSheild()
    {
        var shieldClone = Instantiate(_shieldPrefab, transform.position , Quaternion.identity, transform);
        yield return new WaitForSeconds(10);
        Destroy(shieldClone);
    }

    private IEnumerator EnableThenDoubleSpeed()
    {
        var actualSpeed=this.speed;
        var thrusterClone= Instantiate(_thrusterPrefab, transform.position + new Vector3(0, -3f, 0), Quaternion.identity, transform);
        this.speed = 2* this.speed;
        yield return new WaitForSeconds(10);
        this.speed = actualSpeed;
        Destroy(thrusterClone);
    }

    public void Damage(int value)
    {
        this.health = health - value;
        if (health < 0)
        {
            this.health = 100;
            lifes--;
        }
        if (lifes < 0)
        {
            Destroy(this.gameObject);
            if (_spawnManager != null)
            {
                _spawnManager.DestroyAllSubroutines();
                Destroy(_spawnManager);
            }
        }
    }

}
