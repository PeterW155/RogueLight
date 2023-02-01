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

    private void SpawnEnemy()
    {
        int rand = Random.Range(0, enemies.Length);
        Instantiate(enemies[rand], transform);
    }
}
