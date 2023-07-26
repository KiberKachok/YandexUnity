using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using Random = UnityEngine.Random;


public class RoomHandler : MonoBehaviour
{
    public Vector2 ySpawnClamp; //Ограничение зоны создания новых объектов по оси Y
    public float xSpawnPos; //Место создания новых объектов по оси X
    public float spawnRate; //Мин. и макс. время очередного препятствия/награды

    public Transform obstaclesRoot;
    public GameObject obstaclePrefab;

    public Transform rewardsRoot;
    public GameObject rewardPrefab;


    public void OnGameStarted()
    {
        StartCoroutine(GameProcess());
    }

    //Короутина, генерирующая препятствия и награды
    public IEnumerator GameProcess()
    {
        List<GameObject> dynamicGameObjects = new List<GameObject>();
        float spawnTimer = spawnRate;

        while (true)
        {
            //Генерируем новый объект по счётчику таймера
            if(spawnTimer <= 0)
            {
                if (Random.Range(0f, 1f) >= 0.4f)
                    dynamicGameObjects.Add(SpawnObstacle());
                else
                    dynamicGameObjects.Add(SpawnReward());

                spawnTimer = spawnRate;
            }

            spawnTimer -= Time.deltaTime;
            yield return null;
        }
    }

    public GameObject SpawnObstacle()
    {
        GameObject obstacle = Instantiate(obstaclePrefab, obstaclesRoot);
        obstacle.transform.position = new Vector3(xSpawnPos, 
                                                  Random.Range(ySpawnClamp.x, ySpawnClamp.y),
                                                  0);
        obstacle.GetComponent<ObstacleHandler>().ySpeed *= Random.Range(0f, 1f) > 0.5f ? 1f : -1f;
        return obstacle;
    }

    public GameObject SpawnReward()
    {
        GameObject reward = Instantiate(rewardPrefab, rewardsRoot);
        reward.transform.position = new Vector3(xSpawnPos,
                                                  Random.Range(ySpawnClamp.x, ySpawnClamp.y),
                                                  0);
        //obstacle.GetComponent<ObstacleHandler>().ySpeed *= Random.Range(0f, 1f) > 0.5f ? 1f : -1f;
        return reward;
    }
}
