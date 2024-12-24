using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefualtProjectile : ProjectileBase
{
    public override void Intialize()
    {
        base.Intialize();

        this.speed = 0.7f;
        this.existTime = 1f;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Move()
    {
        base.Move();
    }
}
