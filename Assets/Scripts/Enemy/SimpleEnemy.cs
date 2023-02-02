using System.Collections;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour, IEnemyDamageable
{

    public float health;
    public float speed;
    public float damage;
    public AudioClip _dieSFX;

    protected GameObject _player;
    protected Vector2 _toPlayerDirection;
    protected float _toPlayerDistance;
    private SpriteRenderer _spriteRenderer;
    private IEnumerator _flashRed;
    private bool _spriteFacesLeft;

    protected virtual void Start()
    {
        _player = GameObject.FindGameObjectsWithTag("Player")[0];
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteFacesLeft = !_spriteRenderer.flipX;
    }

    protected virtual void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, _player.transform.position, speed * Time.deltaTime);

        _toPlayerDirection = _player.transform.position - transform.position;
        _toPlayerDistance = _toPlayerDirection.magnitude;
        _toPlayerDirection.Normalize();
        var movingLeft = _toPlayerDirection.x < 0;

        // If is going left but is currently not facing left
        if (movingLeft)
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
        _flashRed = FlashRedForSeconds(0.2f);
        StartCoroutine(_flashRed);
        
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
            yield return null;
        }
    }

    private void KillEnemy()
    {
        _player.GetComponent<AudioSource>().PlayOneShot(_dieSFX);
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
