using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Text pointText;
    public Text lifeText;
    public GameObject gameOverPanel;

    [SerializeField] GameObject bulletPrefab;

    private AudioSource playerAudioSource;
    private Rigidbody2D rb;
    private Animator playerAnimator;
    private BoxCollider2D playerCollider;

    public float leftRightSpeed;
    public float jumbSpeed;
    public float bulletSpeed;
    float playerOriginalScaleX;
    int playerFacingDirection = 1;
    int point = 0;
    int life = 3;

    public LayerMask groundLayer;

    //Audio Clip
    public AudioClip gameBackgroundMusic;
    public AudioClip jumpSFX;
    public AudioClip bulletSFX;
    public AudioClip gameOverSFX;
    public AudioClip collectablesSFX;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();

        playerAudioSource = GetComponent<AudioSource>();

        playerOriginalScaleX = gameObject.transform.localScale.x;

        playerAudioSource.clip = gameBackgroundMusic;
        playerAudioSource.Play();
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerFacingDirection = 1;
            rb.velocity = new Vector2(leftRightSpeed * playerFacingDirection, rb.velocity.y);
            gameObject.transform.localScale = new Vector3(playerOriginalScaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

            PlayAnimation("PlayerWalking", "NoAnimation");
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerFacingDirection = -1;
            rb.velocity = new Vector2(leftRightSpeed * playerFacingDirection, rb.velocity.y);
            gameObject.transform.localScale = new Vector3(-playerOriginalScaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            
            PlayAnimation("PlayerWalking", "NoAnimation");
        }
        else
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
            PlayAnimation("PlayerIdle","NoAnimation");
        }
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(playerCollider.IsTouchingLayers(groundLayer))
            {

            rb.velocity = new Vector2(rb.velocity.x, jumbSpeed);
                // add sfx
                playerAudioSource.PlayOneShot(jumpSFX,1f);
            }
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //Generate Bullet
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            //Add velocity to player facing directions...
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(playerFacingDirection * bulletSpeed, 0f);
            //add sfx
            playerAudioSource.PlayOneShot(bulletSFX,1f);
        } 
    }

    void PlayAnimation(string animationName1 , string animationName2)
    {
        if (playerCollider.IsTouchingLayers(groundLayer))
        {

            playerAnimator.Play(animationName1);
        }
        else
        {
            playerAnimator.Play(animationName2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            point++;
            pointText.text = "Point : " + point;
            Destroy(collision.gameObject);
            playerAudioSource.PlayOneShot(collectablesSFX,1f);
        }
        if(collision.tag == "Enemy")
        {
            if (life == 0)
            {
                gameOverPanel.SetActive(true);
            }
            else
            {
                life--;
                lifeText.text = "Life : " + life;
            }
            
            

            //add sfx
            playerAudioSource.PlayOneShot(gameOverSFX,1f);
        }
        if(collision.tag == "LebelOneComplete")
        {
            //SceneManager.LoadScene(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
//vedio number10--1.15min
