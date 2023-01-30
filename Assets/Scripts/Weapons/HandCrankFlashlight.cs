using UnityEngine;

public class HandCrankFlashlight : Weapon
{
    [SerializeField] private GameObject _damageBoxVisual;
    
    public override void FireDown()
    {
        if (DamageBox)
            DamageBox.enabled = true;
        if (_damageBoxVisual)
            _damageBoxVisual.SetActive(true);
    }

    public override void FireUp()
    {
        if (DamageBox)
            DamageBox.enabled = false;
        if (_damageBoxVisual)
            _damageBoxVisual.SetActive(false);
    }
}
