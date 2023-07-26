using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardHandler : MonoBehaviour
{
    public float xSpeed;
    public float localSpeed;
    public float criticalDistance;

    private BallHandler _ballHandler; 

    void Start()
    {
        _ballHandler = FindObjectOfType<BallHandler>();
    }

    void Update()
    {
        //Передвигаем награду по координатам
        transform.position += new Vector3(xSpeed * Time.deltaTime, 0f, 0f);

        //Передвигаем награду к шарику
        float distance = Vector3.Distance(_ballHandler.transform.position, transform.position);
        if(distance < criticalDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                _ballHandler.transform.position, 
                localSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
