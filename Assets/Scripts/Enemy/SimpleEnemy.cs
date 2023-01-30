using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{

    public float health;
    public float speed;

    private GameObject player;
    private float distance;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void DamageEnemy(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        Destroy(gameObject);
        ScoreManager.instance.AddPoint();
    }
}
