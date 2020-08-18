using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
    private Enemy enemy;
    private float attackTime;
    private float attackCooldown = 3;
    private bool attack = true;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Excute()
    {
        Attack();
        if(enemy.InThrowRange && !enemy.InMeleeRange)
        {
            enemy.ChangeState(new RangedState());
        }
        else if (enemy.Target == null)
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
    private void Attack()
    {
        attackTime += Time.deltaTime;
        if (attackTime >= attackCooldown)
        {
            attack = true;
            attackTime = 0;
        }
        if (attack)
        {
            attack = false;
            enemy.anim.SetTrigger("attack");
        }
    }
}
