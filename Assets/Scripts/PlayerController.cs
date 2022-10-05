using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator TransitionAnimator;

    public Text pointText;
    public Text lifeText;
    public Text totalPointText;
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

    bool isLeftButtonPressed;
    bool isRightButtonPressed;
    bool gameover = false;

    public void LeftButtonPressed()
    {
        isLeftButtonPressed = true;
    }
    public void LeftButtonReleased()
    {
        isLeftButtonPressed = false;
    }
    public void RightButtonPressed()
    {
        isRightButtonPressed = true;
    }
    public void RightButtonReleased()
    {
        isRightButtonPressed = false;
    }

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
        if (!gameover && Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && !gameover || isRightButtonPressed &&!gameover)
        {
            playerFacingDirection = 1;
            rb.velocity = new Vector2(leftRightSpeed * playerFacingDirection, rb.velocity.y);
            gameObject.transform.localScale = new Vector3(playerOriginalScaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

            PlayAnimation("PlayerWalking", "NoAnimation");
        }
        else if (!gameover && Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && !gameover || isLeftButtonPressed && !gameover)
        {
            playerFacingDirection = -1;
            rb.velocity = new Vector2(leftRightSpeed * playerFacingDirection, rb.velocity.y);
            gameObject.transform.localScale = new Vector3(-playerOriginalScaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

            PlayAnimation("PlayerWalking", "NoAnimation");
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            PlayAnimation("PlayerIdle", "NoAnimation");
        }
        //Jump
        if (!gameover && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            JumpButtonPressed();
        }
        if (Input.GetKeyDown(KeyCode.Return) && !gameover)
        {
            ShootButtonPressed();
        }
    }

    public void JumpButtonPressed()
    {
        if (playerCollider.IsTouchingLayers(groundLayer) && !gameover)
        {

            rb.velocity = new Vector2(rb.velocity.x, jumbSpeed);
            // add sfx
            playerAudioSource.PlayOneShot(jumpSFX, 1f);
        }
    }

    public void ShootButtonPressed()
    {
        //Generate Bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //Add velocity to player facing directions...
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(playerFacingDirection * bulletSpeed, 0f);
        //add sfx
        playerAudioSource.PlayOneShot(bulletSFX, 1f);
    }

    void PlayAnimation(string animationName1, string animationName2)
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
        if (collision.tag == "Collectable")
        {
            point++;
            pointText.text = "Point : " + point;
            Destroy(collision.gameObject);
            playerAudioSource.PlayOneShot(collectablesSFX, 1f);
        }
        if (collision.tag == "Enemy")
        {
            if (life == 0)
            {
                gameOverPanel.SetActive(true);
                gameover = true;
                totalPointText.text = "Total Point : " + point;
            }
            else
            {
                life--;
                lifeText.text = "Life : " + life;
            }



            //add sfx
            playerAudioSource.PlayOneShot(gameOverSFX, 1f);
        }
        if (collision.tag == "LebelComplete")
        {
            TransitionAnimator.SetTrigger("DoTransition");
            Invoke("LoadScene", 1.5f);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        /*if (collision.tag == "LebelTwoComplete")
        {
            TransitionAnimator.SetTrigger("DoTransition");
            SceneManager.LoadScene(0);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }*/
    }

    void LoadScene()
    {
        //SceneManager.LoadScene(1);
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(sceneNum);
        if (sceneNum < 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    
    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

