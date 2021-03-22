using UnityEngine;

public abstract class Character : MonoBehaviour
{

    protected Animator myAnimator;

    [SerializeField]
    protected GameObject knifePrefab;

    [SerializeField]
    protected float movementSpeed;

    public bool Attack { get; set; }

    [SerializeField]
    private Transform knifePos;

    protected  bool facingRight;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Debug.Log("Charater Start");
        facingRight = true;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        /*facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        */
    }
    public virtual void ThrowKnife(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Knife>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Knife>().Initialize(Vector2.left);
        }
    }
}
