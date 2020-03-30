using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{

    float directionX;
    float directionY;
    Rigidbody rb;
    public float maxSpeed = 0.5f;
    SpriteRenderer spriteRenderer;
    Color color;

    bool isDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        directionX = (Random.value / 4f) * transform.localScale.x;
        directionY = (Random.value / 4f) * transform.localScale.x;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(CheckAndDestroy());
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(-10, 10) > 8)
        {
            //меняем направление
            directionX = (Random.value / 4f) * transform.localScale.x;
            directionY = (Random.value / 4f) * transform.localScale.x;
        }
        //двигаемся в выбранном направлении
        rb.AddForce(directionX, directionY, 0);
        if (spriteRenderer.color.a < 0.6f && isDestroy == false)
            spriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(spriteRenderer.color.a, 0.6f, Time.deltaTime/0.5f));
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
                isDestroy = true;
                BubbleGenerator.freeLayers.Enqueue(spriteRenderer.sortingOrder);
                BubbleGenerator.currentCountBubble--;
                while (spriteRenderer.color.a>=0)
                {
                    spriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(spriteRenderer.color.a, 0f, Time.deltaTime / 0.5f));
                    yield return new WaitForEndOfFrame();
                }
               
                Destroy(gameObject);

            }            
        }        
    }

    




}
