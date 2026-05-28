using System;
using NUnit.Framework;
using Unity.VisualScripting;
//using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("player variables")]
    [SerializeField] private float jumpHeight  = 10;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float wallSpeed = 2f;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] private GameManager gameManager;
    public bool canMove = true;
    public int coins = 0;
    public int deaths = 0;
    public int storedCoins = 0;
    public bool hasKey = false;


    //public AudioClip jumpSound;
    
    

    [SerializeField] private bool isGrounded;
    
    public bool isAllowedToJump;
    [SerializeField] private bool isAllowedToWallJump;
    public Transform groundCheck;
    public float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    

    [SerializeField] private bool onWall;
    [SerializeField] private Transform wallCheck;
    
    private float wallCheckRadius = 0.4f;
    public static GameManager Instance;
    private static PlayerScript instance;
    [SerializeField] private GameObject PickupSpund;
    public Animator animator;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private AudioSource xaudioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        xaudioSource = GameObject.Find("PickupSound").GetComponent<AudioSource>();
        rb =GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
       
    }
    void FixedUpdate()
    {
        onWall = Physics2D.OverlapCircle(wallCheck.position,wallCheckRadius, groundLayer);

        //checks for walls and ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius, groundLayer);

        /*float vertical = Input.GetAxis("Vertical");
        Vector2 yMove = new Vector2(0, vertical);
        float yVelocity = yMove.magnitude;
        animator.SetFloat("YVelocity",yVelocity);*/
        
        jump();
        sprint();
        
        wallSlide();
        selfDestruct();
      
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" )
        {
            Destroy(gameObject);
        }
        
        if (rb.linearVelocity.x > 0.01f /* && !onWall*/)
        {
            sprite.flipX = false;
        }
        else if (rb.linearVelocity.x < -0.01f)
        {
            sprite.flipX = true;
        }

        float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        animator.SetFloat("YSpeed", rb.linearVelocity.y);
        //animator.SetFloat("Speed", rb.linearVelocity.x);
        
        Vector2 movement = new Vector2(horizontal, 0);
        float xSpeed = movement.magnitude;
        // Set the Animator parameter
        animator.SetFloat("Speed", xSpeed);

        //Side to side movement
        if (canMove)
       {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
       }
       

        
       //setAnimation(moveInput);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Dmg"))
        {
            rb.linearVelocity = Vector2.zero;
            //transform.position = new Vector2(0,0);
            GameManager.Instance.OnPlayerDeath(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trigger"))
        {
            xaudioSource.Play();
            isAllowedToJump = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            xaudioSource.Play();
            coins = coins + 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Key"))
        {
            xaudioSource.Play();
            hasKey = true;
            Destroy(collision.gameObject);
        }
         if (collision.gameObject.CompareTag("Dmg"))
        {
            rb.linearVelocity = Vector2.zero;
            //transform.position = new Vector2(0,0);
            GameManager.Instance.OnPlayerDeath(this);
        }
    }
  
    private void jump()
    {
        float direction = sprite.flipX ? -1 : 1;
        if(Input.GetKeyDown(KeyCode.Space) && isAllowedToWallJump && canMove) //walljump
        {
            
            rb.linearVelocity = new Vector2(direction * 10f, jumpHeight);
            audioSource.Play();
            

        }

        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded  && canMove) // normal jump
         {
            rb.linearVelocity = Vector2.up * jumpHeight;
            audioSource.Play();
            

        }

        else if(Input.GetKeyDown(KeyCode.Space) && isAllowedToJump && !isGrounded && !onWall  && canMove) // double jump
        {
            rb.linearVelocity = Vector2.up * jumpHeight;
            isAllowedToJump = false;
            audioSource.Play();
            

            
        }
    }
    private void wallSlide()
    {       
        if (onWall && !isGrounded && rb.linearVelocity.y < 5) // Input.GetKey(KeyCode.A) && onWall && rb.linearVelocity.y < 2|| Input.GetKey(KeyCode.D) && onWall && rb.linearVelocity.y < 2
        {
            isAllowedToWallJump = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSpeed);
            
        }
        else
        {
            isAllowedToWallJump = false;
        }
    }

    private void sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 10f;
        }
        else
        {
            moveSpeed = 5f;
        }
    }
    private void selfDestruct()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu" && Input.GetKeyDown(KeyCode.Escape))
        {
           GameManager.Instance.OnPlayerDeath(this);
        }
    }




    /*private void setAnimation(float moveInput)
    {
        if(isGrounded)
        { 
            if (Mathf.Abs(moveInput) == 0)
            {
            animator.Play("Knight_Idle");
            }
            else
            {
                animator.Play("Knight_Walk");
            }
        }
        else
        {
            if(rb.linearVelocity.y < 0)
            {
                animator.Play("Knight_Fall");
            }
                
            
        }
    }*/
}
