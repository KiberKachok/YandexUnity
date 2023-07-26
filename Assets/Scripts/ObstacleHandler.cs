using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;
    public Vector2 yClamp;

    private void Update()
    {
        //Передвигаем препятствие по координатам
        transform.position += new Vector3(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0);

        //Переворачиваем направление движения при достижении границы
        if ((ySpeed > 0 && transform.position.y > yClamp.y) ||
            (ySpeed < 0 && transform.position.y < yClamp.x))
            ySpeed = -ySpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
