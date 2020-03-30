using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
    public GameObject[] bubbles;
    public int AmounStartBubbles;
    public PlayerControl Player;
    public float offsetX;
    public float offsetY;
    public int MaxBubble;
    public static int currentCountBubble;

    public static Queue<int> freeLayers = new Queue<int>();

    // Start is called before the first frame update
    void Start()
    {

        Physics.IgnoreLayerCollision(0, 8);
        for (int i = 0; i < AmounStartBubbles; i++)
        {
            GameObject bubble = Instantiate(bubbles[Random.Range(0, bubbles.Length)]);
            bubble.GetComponent<SpriteRenderer>().sortingOrder = -i;
            float scale = Random.Range(0.5f, 10f);
            bubble.transform.localScale = new Vector3(scale, scale, 1);
            bubble.transform.position = new Vector3(Player.transform.position.x + Random.Range(-offsetX*2, offsetX*2), Player.transform.position.y + Random.Range(-offsetY*2, offsetY*2), 0);
            bubble.transform.Rotate(0, 0, Random.Range(0, 360));
            currentCountBubble++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        float currentOffsetX = offsetX ;
        float currentOffsetY = offsetY ;
        if (Player.currentSize > 2)
        {
             currentOffsetX = offsetX * (Player.currentSize / 2);
             currentOffsetY = offsetY * (Player.currentSize / 2);
        }


        Debug.Log(currentCountBubble);
        if (Random.Range(0,10) > 8)
        if (currentCountBubble < MaxBubble && freeLayers.Count!=0)
        {
            GameObject bubble = Instantiate(bubbles[Random.Range(0, bubbles.Length)]);
            bubble.GetComponent<SpriteRenderer>().sortingOrder = freeLayers.Dequeue();


            float scale = Random.Range(0.5f * (Player.currentSize/1.5f), 10f * (Player.currentSize/1.5f));
            bubble.transform.localScale = new Vector3(scale, scale, 1);

            float x = Random.Range(-currentOffsetX, currentOffsetX);
            float y = Random.Range(-currentOffsetY, currentOffsetY);
            if (x < 0)
                x -= currentOffsetX;
            else
                x += currentOffsetX;

            if (y < 0)
                y -= currentOffsetY;
            else
                y += currentOffsetY;
            bubble.transform.position = new Vector3(Player.transform.position.x + x, Player.transform.position.y + y, 0);
            bubble.transform.Rotate(0, 0, Random.Range(0, 360));
            currentCountBubble++;
        }
    }
}
