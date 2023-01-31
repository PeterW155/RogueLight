using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour, IEnemyDamageable, IEnemyKillable
{

    public float health;
    public float speed;
    public float damage;
    public int points;

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
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
        ScoreManager.instance.AddPoint();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
