using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;
    private float patrolTime;
    private float patrolDuration = 10;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Excute()
    {
        Debug.Log("Patroling");
        Patrol();
        enemy.Move();
        if(enemy.Target != null && enemy.InThrowRange)
        {
            enemy.ChangeState(new RangedState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    { 
        if (other.tag == "kunai")
        {
            enemy.Target = Player.Instance.gameObject.gameObject;
        }
    }
    private void Patrol()
    {
        patrolTime += Time.deltaTime;
        if (patrolTime >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
