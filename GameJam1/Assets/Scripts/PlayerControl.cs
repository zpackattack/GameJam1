using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed = 0.1f;
    Rigidbody rb;
    public float currentSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(-Speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0, Speed, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0, -Speed, 0);
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("fired");
        if (collision.gameObject.name == "Enemy")
        {
            Destroy(collision.gameObject);
            currentSize += 0.5f;
            transform.localScale = new Vector3(currentSize, currentSize, 1);
        }

    }
}

