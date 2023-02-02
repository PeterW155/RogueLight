using System.Collections;
using UnityEngine;

public class ToyCamera : Weapon
{
    [SerializeField] private float _activationDuration;
    [SerializeField] private float _cooldownDuration;

    private float _lastActivationTime;

    private void Start()
    {
        // Activation Duration should be shorter than Cooldown Duration
        _activationDuration = Mathf.Min(_activationDuration, _cooldownDuration);
    }

    public override void FireDown()
    {
        base.FireDown();
        
        if (Time.time < _lastActivationTime + _cooldownDuration)
            return;

        if (CurrentRound <= 0)
            return;
        
        CurrentRound--;
        _lastActivationTime = Time.time;
        
        if (DamageBox)
            DamageBox.enabled = true;
        if (_damageBoxVisual)
            _damageBoxVisual.SetActive(true);
        SFXSource.Play();

        StartCoroutine(TurnOff());
    }

    private IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(_activationDuration);
        if (DamageBox)
            DamageBox.enabled = false;
        if (_damageBoxVisual)
            _damageBoxVisual.SetActive(false);
    }
}
