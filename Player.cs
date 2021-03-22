using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private static  Player instance;
    public static  Player Instance
    {
        get { 
            if(instance==null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance; 
        }
        set { instance = value; }
    }

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groudRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private float jumpForce;

    public Rigidbody2D MyRigidbody;


    public float GetForce()
    {
        return jumpForce;
    }


    public bool Slide { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }

    // Start is called before the first frame update
    public override void Start()
    {
        Debug.Log("Player Start");
        base.Start();
        movementSpeed = 7;

        MyRigidbody = GetComponent<Rigidbody2D>();
        
    }
    void Update()
    {
        HandleInput();
        FixUpdate();
    }

    // Update is called once per frame
    void FixUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");

        OnGround = IsGround();

        HandleMoment(horizontal);

        Flip(horizontal);

        HandleLayers();
    }
    private void HandleMoment(float horizontal)
    {
        if (MyRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }
        if (!Attack && !Slide)
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }
        //if (Jump && MyRigidbody.velocity.y == 0 && OnGround)
        //{

        //}
        
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            myAnimator.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            myAnimator.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            myAnimator.SetTrigger("jump");
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            myAnimator.SetTrigger("throw");
        }
    }

    private bool IsGround()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] collider = Physics2D.OverlapCircleAll(point.position, groudRadius, whatIsGround);
                for (int i = 0; i < collider.Length; i++)
                {
                    if (collider[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void HandleLayers()
    {
        if (!OnGround)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
    public override void ThrowKnife(int value)
    {
        if(!OnGround && value==1 || OnGround && value==0)
        {
            base.ThrowKnife(value);
        }

    }
}
