using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    float directionX;
    float directionY;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        directionX = Random.Range(-5,5);
        directionY = Random.Range(-5, 5);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(-10,10) > 6)
        {
            directionX = Random.Range(-5, 5);
            directionY = Random.Range(-5, 5);
        }

        //if (Random.Range(0,2) > 1)
            rb.AddForce(directionX, directionY, 0);
    }
}
