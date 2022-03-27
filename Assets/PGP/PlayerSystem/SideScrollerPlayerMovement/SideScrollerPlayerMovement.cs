using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SideScrollerPlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed, jumpForce;
    public int jumpLimit;
    int jumpLeft;
    float moveInput;
    bool facingRight, jump;
    
    bool isGrounded;
    public bool canDash;
    public Transform groundCheck;
    [SerializeField] Transform AvatarTF;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public float coT = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        jumpLeft = jumpLimit;
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.W) && jumpLeft > 0) jump = true;
        if (Input.GetKeyDown(KeyCode.Space)) Dash();
    }

    private void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded) jumpLeft = jumpLimit; ;

        if (jump && jumpLeft > 0)
        {
            jump = false;
            rb.velocity = Vector2.up * jumpForce;
            jumpLeft--;
            PGSoundManager.PlaySound("Jump",false, 1f);
        }



        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if((facingRight == false && moveInput > 0) || (facingRight == true && moveInput < 0))
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        AvatarTF.localScale = PGTool.ChangeVector(AvatarTF.localScale, 1, -AvatarTF.localScale.x);
    }

    void Dash()
    {
        if (!canDash) return;
        canDash = false;
        StartCoroutine(InDash(Camera.main.ScreenToWorldPoint(Input.mousePosition)));

    }

    IEnumerator InDash(Vector2 toward)
    {
        print(toward);
        coT = 0;
        toward = (toward - (Vector2)transform.position).normalized;
        print(toward);
        while (coT < 0.06)
        {
            coT += Time.deltaTime;

            transform.position += (Vector3)toward * Time.deltaTime * 75;

            yield return new WaitForSeconds(0);
        }
    }

    public void StopDash()
    {
        print("stop");
        coT = 10;
    }

}
