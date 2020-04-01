using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float MaxRandom = 100;
    public PlayerControl player;
    public int startAmountEnemy = 20;
    public float offsetX;
    public float offsetY;
    public int maxEnemyCount = 200;
    public static int currentEnemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startAmountEnemy; i++)
        {
            //Если удалось загружаем из папки Resources префаб Enemy, создаем его на сцене
            //и сохраняем как игровой объект
            GameObject enemy = Instantiate(Resources.Load("Enemy")) as GameObject;
            //Получаем доступ к копмоненту SpriteRenderer, который отвечает за визуализацию
            //спрайта в сцене
            SpriteRenderer spriteRender = enemy.GetComponent<SpriteRenderer>();
            //Генерируем спрайту случайный цвет
            spriteRender.color = new Color(Random.value, Random.value, Random.value);
            //Генерируем спрайту случайное положение
            enemy.transform.position = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0);
            float scale = Random.Range(player.currentSize * 0.5f, player.currentSize + 1f);
            enemy.transform.localScale = new Vector3(scale, scale, 1);
            currentEnemyCount++;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentEnemyCount< maxEnemyCount)
        //Пробуем на текущем кадре создать нового противника
        if (Random.Range(1,100) < 5)
        {
            //Если удалось загружаем из папки Resources префаб Enemy, создаем его на сцене
            //и сохраняем как игровой объект
            GameObject enemy = Instantiate(Resources.Load("Enemy")) as GameObject;
            //Получаем доступ к копмоненту SpriteRenderer, который отвечает за визуализацию
            //спрайта в сцене
            SpriteRenderer spriteRender = enemy.GetComponent<SpriteRenderer>();
            //Генерируем спрайту случайный цвет
            spriteRender.color = new Color(Random.value, Random.value, Random.value);

                /*
                 * 25 - 1
                 * x  - 2
                 */
                float currentOffsetX = offsetX;
                float currentOffsetY = offsetY;
                if (player.currentSize > 2)
                {
                    currentOffsetX = offsetX * (player.currentSize / 2);
                    currentOffsetY = offsetY * (player.currentSize / 2);
                }
                //Генерируем спрайту случайное положение
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


            enemy.transform.position = new Vector3(player.transform.position.x + x, player.transform.position.y + y, 0);
            float scale = Random.Range(player.currentSize * 0.5f, player.currentSize * 1.75f);
                // Debug.Log(scale);
            enemy.transform.localScale = new Vector3(scale, scale, 1);
                currentEnemyCount++; 
        }
    }
}
