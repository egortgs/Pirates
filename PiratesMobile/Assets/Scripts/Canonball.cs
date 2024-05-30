using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canonball : Ammo
{
    private void Start()
    {

    }
    private void FixedUpdate()
    {
        ammoRB.velocity = transform.forward * ammoForce;
    }



}
