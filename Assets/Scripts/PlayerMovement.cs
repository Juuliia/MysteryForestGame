using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 6f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform stick;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private GameObject objectToAppearPause;
    
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myFeetCollider;
    public bool isAlive = true;
    bool canShoot = true;
    bool isPaused;
    bool isCharging;
   bool wasMovingLastFrame;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();

       
    }

    void Update()
    {
        Run();
        FlipSprite();
    }


   
    void OnMove(InputValue value)
    {
        if(isPaused || isCharging)
        {
            
            return;
        }
        
        moveInput = value.Get<Vector2>();
         
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.enabled = true;
            audioSource.Play();
        }
        
        
    }

void OnFire(InputValue value)
{
    if(!isAlive || !canShoot){return;}
   Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    // Calculate the direction from player to mouse
    Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

    // Set the bullet direction
    myAnimator.SetTrigger("isAttacking");   
    FireBullet(direction);

    StartCoroutine(Cooldown());
}

void FireBullet(Vector2 direction)
{
    // Instantiate the bullet and set its direction
    GameObject bulletInstance = Instantiate(bullet, stick.position, Quaternion.identity);
    Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
    bulletScript.SetDirection(direction);
}
IEnumerator Cooldown()
{
    canShoot = false;
    yield return new WaitForSeconds(0.8f);
    canShoot = true;

}

    void Run()
{
     
    Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, moveInput.y * runSpeed);
    myRigidbody.velocity = playerVelocity;

   
    bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon || Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

    myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);

    if (!playerHasHorizontalSpeed && wasMovingLastFrame)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        wasMovingLastFrame = playerHasHorizontalSpeed;
}

void FlipSprite()
{
    bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    if(playerHasHorizontalSpeed)
    {

    
    transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
}
}

void OnPause()
{
     isPaused = !isPaused; // Toggle the paused state

        if (isPaused)
        {
             objectToAppearPause.SetActive(true);
            Time.timeScale = 0; // Pause the game
            
            
        }
        else
        {
            objectToAppearPause.SetActive(false);
            Time.timeScale = 1; // Unpause the game
            
        }
    
}

public void ResetTime()
{
    Time.timeScale = 1;
}

void OnCharge()
{
    isCharging = !isCharging;
    if(isCharging)
    {
        myAnimator.SetBool("isCharging", true);
    }
    else
    {
         myAnimator.SetBool("isCharging", false);
    }

    
}

 void OnQuit()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
