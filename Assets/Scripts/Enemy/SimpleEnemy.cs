using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{

    public float health;
    public float speed;

    private GameObject player;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void DamageEnemy(float damage)
    {
        health = -damage;
        if(health <= 0)
        {
            EnemyDie();
        }
    }

    public void EnemyDie()
    {
        Destroy(this.gameObject);
    }
}
