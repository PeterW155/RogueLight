using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage { get; set; }
    public float Speed { get; set; }

    private Transform _transform;

    private void Start() => _transform = transform;

    private void Update()
    {
        transform.position += _transform.up * (Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<IEnemyDamageable>().DamageEnemy(Damage);
            Destroy(gameObject);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
