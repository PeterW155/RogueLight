using System.Collections;
using UnityEngine;

public class HandCrankFlashlight : Weapon
{
    [SerializeField] private GameObject _damageBoxVisual;
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
        if (Time.time < _lastActivationTime + _cooldownDuration)
            return;

        _lastActivationTime = Time.time;
        
        if (DamageBox)
            DamageBox.enabled = true;
        if (_damageBoxVisual)
            _damageBoxVisual.SetActive(true);

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

    public override void FireUp() { }
}
