using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
     public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float climbSpeed = 2f;
    private bool isJumping = false;
    private bool isClimbing = false;
    private bool isPushing = false;

    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Movimiento horizontal
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Salto
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }

        // Escalar
        if (isClimbing)
        {
            float moveY = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, moveY * climbSpeed);
        }

        // Empujar objetos
        if (Input.GetKeyDown(KeyCode.E) && isPushing)
        {
            animator.SetTrigger("Push");
            // Código para empujar objetos
        }

        // Actualizar parámetros del animator
        animator.SetFloat("Speed", Mathf.Abs(moveX));
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsClimbing", isClimbing);
        animator.SetBool("IsPushing", isPushing);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reiniciar salto al tocar el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Habilitar escalada
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isClimbing = true;
            rb.gravityScale = 0f;
            animator.SetBool("IsClimbing", true);
        }
        
        // Habilitar empuje de objetos
        if (collision.gameObject.CompareTag("Box"))
        {
            isPushing = true;
            animator.SetBool("IsPushing", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Deshabilitar escalada
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.gravityScale = 1f;
            animator.SetBool("IsClimbing", false);
        }
        
        // Deshabilitar empuje de objetos
        if (collision.gameObject.CompareTag("Box"))
        {
            isPushing = false;
            animator.SetBool("IsPushing", false);
        }
    }
}
