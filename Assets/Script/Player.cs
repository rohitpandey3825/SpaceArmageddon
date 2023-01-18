using Assets.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float fireRate = .5f ;
    private float nextFire = 0 ;
    private bool isTripleShotEnabled = false;
    private bool isSheildACtive = false;

    
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
    private UIManager _uiManger;
    // Start is called before the first frame update
    void Start()
    {
        this.speed = 20 * Time.deltaTime;
        transform.position = new Vector3(0, 0, 0);
        _uiManger = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManger == null)
        {
            print("UI maager is Null");
        }
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            print("spawn maager is Null");
        }
        this._shieldPrefab.SetActive(false);
        this._thrusterPrefab.SetActive(false);
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
        _uiManger.enablePowerUp(powerUp);
    }

    private IEnumerator EnableThenDisableTripleShot()
    {
        this.isTripleShotEnabled = true;
        yield return new WaitForSeconds(10);
        this.isTripleShotEnabled = false;
        _uiManger.DisablePowerUp();
    }

    private IEnumerator EnableThenDisableSheild()
    {
        this.isSheildACtive = true;
        //var shieldClone = Instantiate(_shieldPrefab, transform.position , Quaternion.identity, transform);
        this._shieldPrefab.SetActive(true);
        yield return new WaitForSeconds(10);
        //Destroy(shieldClone);
        this._shieldPrefab.SetActive(false);
        this.isSheildACtive = false;
        _uiManger.DisablePowerUp();
    }

    private IEnumerator EnableThenDoubleSpeed()
    {
        var actualSpeed=this.speed;
        //var thrusterClone= Instantiate(_thrusterPrefab, transform.position + new Vector3(0, -3f, 0), Quaternion.identity, transform);
        this._thrusterPrefab.SetActive(true);
        this.speed = 2* this.speed;
        yield return new WaitForSeconds(10);
        this.speed = actualSpeed;
        this._thrusterPrefab.SetActive(false);
        //Destroy(thrusterClone);
        _uiManger.DisablePowerUp();
    }

    public void incrementScore()
    {
        if(_uiManger!=null)
        {
            _uiManger.incrimentScore();
        }
    }

    public void Damage(int value)
    {
        if (isSheildACtive) return;
        if(!_uiManger.decrimentHealth(value))
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
