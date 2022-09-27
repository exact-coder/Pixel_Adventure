using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator playerAnimator;
    private BoxCollider2D playerCollider;

    public float leftRightSpeed;
    public float jumbSpeed;
    float playerOriginalScaleX;
    int playerFacingDirection = 1;

    public LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();

        playerOriginalScaleX = gameObject.transform.localScale.x;
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerFacingDirection = 1;
            rb.velocity = new Vector2(leftRightSpeed * playerFacingDirection, rb.velocity.y);
            gameObject.transform.localScale = new Vector3(playerOriginalScaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            playerAnimator.Play("PlayerWalking");
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerFacingDirection = -1;
            rb.velocity = new Vector2(leftRightSpeed * playerFacingDirection, rb.velocity.y);
            gameObject.transform.localScale = new Vector3(-playerOriginalScaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            playerAnimator.Play("PlayerWalking");
        }
        else
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
            playerAnimator.Play("PlayerIdle");
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(playerCollider.IsTouchingLayers(groundLayer))
            {

            rb.velocity = new Vector2(rb.velocity.x, jumbSpeed);
            }
        }
    }
}

