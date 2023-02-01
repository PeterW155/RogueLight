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
    private bool _spriteFacesLeft;

    protected void Start()
    {
        _player = GameObject.FindGameObjectsWithTag("Player")[0];
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteFacesLeft = !_spriteRenderer.flipX;
    }

    protected void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, _player.transform.position, speed * Time.deltaTime);

        float xDirection = (_player.transform.position - transform.position).x;
        
        // If is going left but is currently not facing left
        if (xDirection < 0)
        {
            if (_spriteFacesLeft && _spriteRenderer.flipX)
                _spriteRenderer.flipX = false;
            else if (!_spriteFacesLeft && !_spriteRenderer.flipX)
                _spriteRenderer.flipX = true;
        }
        else
        {
            if (_spriteFacesLeft && !_spriteRenderer.flipX)
                _spriteRenderer.flipX = true;
            else if (!_spriteFacesLeft && _spriteRenderer.flipX)
                _spriteRenderer.flipX = false;
        }
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
        ScoreManager.instance.AddPoint();
        DropManager.Instance.NotifyDeath(this);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
