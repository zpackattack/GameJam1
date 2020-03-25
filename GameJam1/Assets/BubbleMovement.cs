using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{

    float directionX;
    float directionY;
    Rigidbody rb;
    public float maxSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        directionX = Random.value / 4f;
        directionY = Random.value / 4f;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(CheckAndDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(-10, 10) > 8)
        {
            //меняем направление
            directionX = Random.value / 4f;
            directionY = Random.value / 4f;
        }
        //двигаемся в выбранном направлении
        rb.AddForce(directionX, directionY, 0);       
    }

    IEnumerator CheckAndDestroy()
    {                
        while (true)
        {
            yield return new WaitForSeconds(5);
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (!onScreen)
            {
                BubbleGenerator.currentCountBubble--;
                Destroy(gameObject);
            }
            
        }
        //asdf
    }
}
