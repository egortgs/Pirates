using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCanon : Weapon
{
    
    protected override void OnShoot()
    {
        GameObject ball = Instantiate(ammo, transform.position, transform.rotation);
        shootVFX.Play();
        sound.PlayOneShot(shootSound);
    }

    
}
