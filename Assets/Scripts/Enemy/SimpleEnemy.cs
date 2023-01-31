using System.Collections;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour, IEnemyDamageable
{

    public float health;
    public float speed;
    public float damage;

    private GameObject _player;
    private SpriteRenderer _spriteRenderer;
    private IEnumerator _flashRed;

    protected void Start()
    {
        _player = GameObject.FindGameObjectsWithTag("Player")[0];
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, _player.transform.position, speed * Time.deltaTime);
    }

    public void DamageEnemy(float damage)
    {
        if (_flashRed != null)
            StopCoroutine(_flashRed);
        StartCoroutine(FlashRedForSeconds(0.2f));
        
        health -= damage;
        if(health <= 0)
        {
            KillEnemy();
        }
        
        IEnumerator FlashRedForSeconds(float seconds)
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(seconds);
            _spriteRenderer.color = Color.white;
        }
    }

    private void KillEnemy()
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
