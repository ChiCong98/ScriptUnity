﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;

    private float idleTimer;

    private float idleDuration=5;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Idle();
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collision2D other)
    {
        
    }
    private void Idle()
    {
        enemy.MyAnimator.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;

        if(idleTimer>=idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
