using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float MaxRandom = 100;
    public PlayerControl player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Пробуем на текущем кадре создать нового противника
        if (Random.Range(1,100) == 1)
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
            enemy.transform.position = new Vector3(Random.Range(-50,50), Random.Range(-50, 50), 0);
            float scale = Random.Range(0.5f, player.currentSize + 1f);
            enemy.transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}
