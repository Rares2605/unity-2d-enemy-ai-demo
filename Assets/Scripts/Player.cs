
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerMovementSpeed = 2f;
    private bool knockbackOk = false;
    public float knockbackForce;
    private float horizontalInput;
    private bool jumpInput;
    private bool facingRight;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float directionX;
    public Transform enemy;
    public float jumpForce = 2f;
    private bool ok=true;
    
   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ok = true;
    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) jumpInput = true; 
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        directionX = transform.position.x - enemy.position.x;
        
        
        if (knockbackOk == false && isGrounded)
        {
            
            rb.linearVelocity = new Vector2(horizontalInput * playerMovementSpeed, rb.linearVelocity.y);
        }

       if (jumpInput &&  ok==true)
        {
            rb.linearVelocity = new Vector2(horizontalInput * playerMovementSpeed, jumpForce);
            
        }
        jumpInput = false;
       
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            knockbackOk = true;
            
            StartCoroutine(KnockbackTime());
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }
    IEnumerator KnockbackTime()
    {
        if (directionX > 0f)

        {
          
            rb.linearVelocity = new Vector2(knockbackForce, knockbackForce);
            isGrounded = false;
        }
        if (directionX < 0f)
        {
            
            rb.linearVelocity = new Vector2(-knockbackForce, knockbackForce);
            isGrounded = false;
        }
        yield return new WaitForSeconds(0.75f);
        knockbackOk = false;
        
    }

  
}
