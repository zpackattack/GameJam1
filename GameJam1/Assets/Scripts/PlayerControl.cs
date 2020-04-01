using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float Speed = 0.1f;
    Rigidbody rb;
    public float currentSize = 1;
    public new Camera camera;
    public AudioClip playerAttack;
    public AudioClip enemyAttack;
    public GameObject BubblePop;
    new AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentSpeed = Speed;
        if (currentSize > 2)
        {
            camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, new Vector3(0, 0, 50 * (currentSize / 2)), Time.deltaTime * 0.5f);
           
        }
        currentSpeed = Speed * currentSize;

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(currentSize, currentSize, 1), Time.deltaTime * 2);
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(-currentSpeed, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0, currentSpeed, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(currentSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0, -currentSpeed, 0);
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
                audio.clip = playerAttack;
                //тогда, удаляем со сцены объект, с которым столкнулись
                
                GameObject current = Instantiate<GameObject>(BubblePop);
                ParticleSystem particle =  current.GetComponent<ParticleSystem>();
                var main = particle.main;
                main.startSize = collision.gameObject.transform.localScale.x*5;                
                current.transform.position = collision.gameObject.transform.position;
                Destroy(current, 1);
                Destroy(collision.gameObject);
                EnemyManager.currentEnemyCount--;
                //Увеличиваем значение переменной, которая отвечает за размер игрока
                 //0.5      1    = 0.5 =  0.5 / 
                 //0.9      1    = 0.1 = 0.9
                 //0.5      4    = 3.5 = 0.125
                 //
                //transform.localScale - collision.transform.localScale.x
                currentSize += collision.transform.localScale.x / transform.localScale.x / 2;
                //Устанавливаем новый размер с помощью изменения масштаба игрока                               

            }
            else
            {
                audio.clip = enemyAttack;
                currentSize -= 0.2f;
                //Устанавливаем новый размер с помощью изменения масштаба игрока
                

                if (currentSize <= 0)
                {
                    SceneManager.LoadScene(4);
                }
            }
            audio.Play();

            
                
        }
        
                
    }
}

