using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed = 0.1f;
    Rigidbody rb;
    public float currentSize = 1;
    public Camera camera;

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
        //Проверяем, что объект с которым мы столкнулись в имени содержит слово Enemy
        if (collision.gameObject.name.Contains("Enemy"))
        {

            if (currentSize >= collision.gameObject.transform.localScale.x)
            {

                //тогда, удаляем со сцены объект, с которым столкнулись
                Destroy(collision.gameObject);
                //Увеличиваем значение переменной, которая отвечает за размер игрока
                currentSize += 0.1f;
                //Устанавливаем новый размер с помощью изменения масштаба игрока
                transform.localScale = new Vector3(currentSize, currentSize, 1);
            }
            else
            {
                currentSize -= 0.2f;
                //Устанавливаем новый размер с помощью изменения масштаба игрока
                transform.localScale = new Vector3(currentSize, currentSize, 1);

                if (currentSize <= 0)
                {
                    Debug.Log("Game over");
                }
            }

            if (currentSize>2)
            camera.transform.localPosition = new Vector3(0, 0, 50 * (currentSize/2));
        }
        
                
    }
}

