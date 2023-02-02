using UnityEngine;
public class Laser : Weapon
{
    [SerializeField] private float _cooldownDuration;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Projectile _laserBeam;
    [SerializeField] private float _beamLiftSpan;
    [SerializeField] private float _beamSpeed;
    
    private float _lastActivationTime;
    
    public override void FireDown()
    {
        base.FireDown();
        
        if (Time.time < _lastActivationTime + _cooldownDuration)
            return;

        if (CurrentRound <= 0)
            return;
        
        CurrentRound--;
        _lastActivationTime = Time.time;
        SFXSource.Play();

        var beam = Instantiate(_laserBeam, _muzzle.position, _muzzle.rotation);
        beam.Damage = Damage;
        beam.Speed = _beamSpeed;
        Destroy(beam, _beamLiftSpan);
    }
}
