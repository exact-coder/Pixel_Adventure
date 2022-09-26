using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float leftRightSpeed;
    public float jumbSpeed;
    float playerOriginalScaleX;
    int playerFacingDirection = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerOriginalScaleX = gameObject.transform.localScale.x;
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerFacingDirection = 1;
            rb.velocity = new Vector2(leftRightSpeed * playerFacingDirection, rb.velocity.y);
            gameObject.transform.localScale = new Vector3(playerOriginalScaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerFacingDirection = -1;
            rb.velocity = new Vector2(leftRightSpeed * playerFacingDirection, rb.velocity.y);
            gameObject.transform.localScale = new Vector3(-playerOriginalScaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumbSpeed);
        }
    }
}
//length is 32min
