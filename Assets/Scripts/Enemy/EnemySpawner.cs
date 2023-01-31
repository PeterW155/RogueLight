using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float maxTime;
    public float timer;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 1 * Time.deltaTime;

        if(timer <= 0.0)
        {
            SpawnEnemy();
            timer = maxTime;
            maxTime -= 0.5f;
            maxTime = Mathf.Max(maxTime, 0.0f);
        }
    }

    public void SpawnEnemy()
    {
        int rand = (int)Random.Range(0, 10);
        if(rand <= 4)
        {
            Instantiate(enemies[0], this.transform);
        }
        else if(rand <= 8)
        {
            Instantiate(enemies[1], this.transform);
        }
        else if (rand <= 10)
        {
            Instantiate(enemies[2], this.transform);
        }
    }
}
