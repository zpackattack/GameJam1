using System.Collections;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    float directionX;
    float directionY;
    float minDistance = 15;
    Rigidbody rb;

    PlayerControl player;
    Rigidbody playerRigidBody;
    public float speed = 5;
    SpriteRenderer spriteRenderer;
    bool isDestroy = false;
    Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        directionX = Random.Range(-5,5);
        directionY = Random.Range(-5,5);
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerControl>();
        playerRigidBody = player.GetComponent<Rigidbody>();
        StartCoroutine(CheckAndDestroy());
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentColor = spriteRenderer.color;
        spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0);
      
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.color.a <= 1f && isDestroy == false)
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(spriteRenderer.color.a, 1f, Time.deltaTime / 0.5f));
            

        float currentSpeed = speed;
        float currentMinDistance = minDistance;
        if (transform.localScale.x > 2)
        {
            currentSpeed = speed * (transform.localScale.x);
            currentMinDistance = minDistance * (player.currentSize/2);
        }
        //Находим дистанцию между игроком и текущей бактерией.
        float distance = Vector3.Distance(player.transform.position, transform.position);

        //Если дистанция до игрока меньше минимальной
        if (distance <= currentMinDistance)
        {
            //Получаем вектор направления движения игрока
            Vector3 velocity = playerRigidBody.velocity;

            //Если размер игрока больше текущей бактерии
            if (player.transform.localScale.x > transform.localScale.x)
            {
                
                //Принимаем случайное решение о том что делать дальше:
                //либо уходить в протовоположенную сторону от игрока, или попытаться выбрать другое случайное направление
                if (Random.Range(1, 10)  > 6.5f)
                {
                    //Двигаемся в ту же сторону и с той же скоростью, что и игрок, тем самым уходя от него
                    rb.AddForce((transform.position - player.transform.position)*player.transform.localScale.x/2);
                }
                else
                {
                    //Принимаем случайное решение, о том, нужно ли менять текущее направление
                    if (Random.Range(-10, 10) > 4)
                    {
                        //меняем направление
                        directionX = Random.Range(-currentSpeed, currentSpeed);
                        directionY = Random.Range(-currentSpeed, currentSpeed);
                    }
                    //двигаемся в выбранном направлении
                    rb.AddForce(directionX, directionY, 0);
                }
            }
            //Если игрок меньше текущей бактерии
            else
            {
                //Принимаем случайное решение о том что делать дальше:
                //либо целенаправлено двигаемся к игроку, или попытаться выбрать другое случайное направление
                if (Random.Range(1, 10) > 6.5f)
                {
                    //Двигаемся в ту же сторону и с той же скоростью, что и игрок, тем самым уходя от него
                    rb.AddForce(player.transform.position - transform.position);
                }
                else
                {
                    //Принимаем случайное решение, о том, нужно ли менять текущее направление
                    if (Random.Range(-10, 10) > 8)
                    {
                        //меняем направление
                        directionX = Random.Range(-currentSpeed, currentSpeed);
                        directionY = Random.Range(-currentSpeed, currentSpeed);
                    }
                    //двигаемся в выбранном направлении
                    rb.AddForce(directionX, directionY, 0);
                }
            }
        }
        //Если дистанция до игрока больше минимальной (условно игрок вне зоны видимости)
        else
        {
            //Двигаемся случайно, иногда выбирая новое направление
            if (Random.Range(-10, 10) > 6)
            {
                directionX = Random.Range(-5, 5);
                directionY = Random.Range(-5, 5);
            }
            rb.AddForce(directionX, directionY, 0);
        }                
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
                while (spriteRenderer.color.a > 0)
                {
                    spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(spriteRenderer.color.a, -0.1f, Time.deltaTime / 0.5f));
                    yield return new WaitForEndOfFrame();
                }
                Destroy(gameObject);
                EnemyManager.currentEnemyCount--;
            }

        }
        //asdf
    }
}
 