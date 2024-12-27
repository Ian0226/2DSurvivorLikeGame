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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                _survivorLikeGame.GetCurrentInGameEnemies(System.Int32.Parse(collision.gameObject.name.Split("_")[1]))
                    .TakeDamage(this.damage);
                existTime = 0;//¾P·´¤l¼u
                break;
        }
    }
}
