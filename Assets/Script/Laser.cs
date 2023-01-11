using Assets.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        this.speed = 25 * Time.deltaTime;//      CommonExtension.getRandomSpeed(1,40);
    }

    // Update is called once per frame
    void Update()
    {
        reposition();
        recycle();
    }

    private void recycle()
    {
        if (this.transform.position.y > 20f)
        {
            this.DestroyLaser();
        }
    }

    public void DestroyLaser()
    {
        Transform parent = transform.parent;
        if(parent != null && parent.name.Contains("TripleShot"))
        {
            Destroy(parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void reposition()
    {
        transform.Translate(Vector3.up * this.speed);
    }
}
