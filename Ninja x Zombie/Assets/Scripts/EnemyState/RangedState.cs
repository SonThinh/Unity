using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;
    private float throwTime;
    private float throwCooldown = 3;
    private bool Throw = true;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Excute()
    {
        ThrowSth();
        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new MeleeState());
        }
        else if (enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
    private void ThrowSth()
    {
        throwTime += Time.deltaTime;
        if(throwTime >= throwCooldown)
        {
            Throw = true;
            throwTime = 0;
        }
        if (Throw)
        {
            Throw = false;
            enemy.anim.SetTrigger("throw");
        }
    }

}
