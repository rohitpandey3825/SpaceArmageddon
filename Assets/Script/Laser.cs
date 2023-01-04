using Assets.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        this.speed = 20 * Time.deltaTime;//      CommonExtension.getRandomSpeed(1,40);
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
            Destroy(gameObject);
        }
    }

    private void reposition()
    {
        transform.Translate(Vector3.up * this.speed);
    }
}
