using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    float directionX;
    float directionY;
    float minDistance = 15;
    Rigidbody rb;

    PlayerControl player;
    Rigidbody playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        directionX = Random.Range(-5,5);
        directionY = Random.Range(-5, 5);
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerControl>();
        playerRigidBody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
        //Находим дистанцию между игроком и текущей бактерией.
        float distance = Vector3.Distance(player.transform.position, transform.position);

        //Если дистанция до игрока меньше минимальной
        if (distance <= minDistance)
        {
            //Получаем вектор направления движения игрока
            Vector3 velocity = playerRigidBody.velocity;

            //Если размер игрока больше текущей бактерии
            if (player.transform.localScale.x > transform.localScale.x)
            {
                
                //Принимаем случайное решение о том что делать дальше:
                //либо уходить в протовоположенную сторону от игрока, или попытаться выбрать другое случайное направление
                if (Random.Range(1, 10)  > 7)
                {
                    //Двигаемся в ту же сторону и с той же скоростью, что и игрок, тем самым уходя от него
                    rb.AddForce(transform.position - player.transform.position);
                }
                else
                {
                    //Принимаем случайное решение, о том, нужно ли менять текущее направление
                    if (Random.Range(-10, 10) > 8)
                    {
                        //меняем направление
                        directionX = Random.Range(-5, 5);
                        directionY = Random.Range(-5, 5);
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
                if (Random.Range(1, 10) > 7)
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
                        directionX = Random.Range(-5, 5);
                        directionY = Random.Range(-5, 5);
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
}
 