using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;
    private float idleTime;
    private float idleDuration = 10;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Excute()
    {
        Debug.Log("Idling");
        Idle();
        if(enemy.Target != null)
        {
            enemy.ChangeState(new PatrolState());
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

    private void Idle()
    {
        enemy.anim.SetFloat("speed", 0);
        idleTime += Time.deltaTime;
        if(idleTime >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
