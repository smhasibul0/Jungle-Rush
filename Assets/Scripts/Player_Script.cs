using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public Animator animator;

    public Rigidbody2D rb;
    public float jumpHeight = 5f;
    private bool isGround = true;


    private float move;
    public float moveSpeed = 5f;
    private bool facingRight = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");

        if (move < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180, 0f);
            facingRight = false;
        }
        else if (move > 0f && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }

        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            jump();
            isGround = false;
            animator.SetBool("jump", true);
        }

        if (Mathf.Abs(move) > 0.1f)
        {
            animator.SetFloat("run", 1f);
        }
        else if (move < .1f)
        {
            animator.SetFloat("run", 0f);
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(move, 0f, 0f) * Time.fixedDeltaTime * moveSpeed;
    }

    void jump()
    {
        rb.AddForce(new Vector2 (0f, jumpHeight), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            animator.SetBool("jump", false);
        }
    }
}
