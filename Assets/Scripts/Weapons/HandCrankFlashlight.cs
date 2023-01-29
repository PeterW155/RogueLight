using UnityEngine;

public class HandCrankFlashlight : Weapon
{
    public override void FireDown()
    {
        if (DamageBox)
            DamageBox.enabled = true;
    }

    public override void FireUp()
    {
        if (DamageBox)
            DamageBox.enabled = false;
    }
}
