using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private IEnemyState currentSate;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        currentSate.Execute();
    }
    public void ChangeState(IEnemyState newState)
    {
        if(currentSate!=null)
        {
            currentSate.Exit();
        }
        currentSate = newState;

        currentSate.Enter(this);
    }
    public void Move()
    {
        MyAnimator.SetFloat("speed", 1);

        transform.Translate(GetDirection()*(movementSpeed*Time.deltaTime));
    }
    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }    
}
