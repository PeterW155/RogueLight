using UnityEngine;

public class RangeEnemy : SimpleEnemy
{
    [SerializeField] private Transform _bowRotation;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private float _shootingRange;
    [SerializeField] private float _cooldownDuration;
    [SerializeField] private Projectile _arrow;
    [SerializeField] private float _arrowSpeed;
    [SerializeField] private AudioSource _launchSFXSource;

    private float _lastLaunchTime;

    protected override void Update()
    {
        base.Update();
        RotateBow();

        if (_toPlayerDistance < _shootingRange && Time.time > _lastLaunchTime + _cooldownDuration)
        {
            _lastLaunchTime = Time.time;
            Launch();
        }
    }

    private void RotateBow()
    {
        var angle = Mathf.Atan2(_toPlayerDirection.y, _toPlayerDirection.x) * Mathf.Rad2Deg - 90.0f;
        var quaternion = Quaternion.Euler(0.0f, 0.0f, angle);
        _bowRotation.rotation = quaternion;
    }

    private void Launch()
    {
        _launchSFXSource.Play();
        var arrow = Instantiate(_arrow, _muzzle.position, _muzzle.rotation);
        arrow.Damage = damage;
        arrow.Speed = _arrowSpeed;
        Destroy(arrow, 2.0f);
    }
}