using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
[SerializeField] float moveSpeed = 5f;
[SerializeField] float minTime = 1f;
[SerializeField] float maxTime = 5f;
Collider2D[] colliders;
Vector3 direction2;
Animator myAnimator;
 Vector2 direction;
float timer;
bool isFacingRight;
 bool isStopped = false;
public bool isDead = false;
ChasePlayer chasePlayer;
 Vector3 initialScale;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        direction = Random.insideUnitSphere;
        timer = Random.Range(minTime, maxTime);
        colliders = GetComponents<Collider2D>();
        chasePlayer = GetComponent<ChasePlayer>();
       
       initialScale = transform.localScale;
    }

    public void IsDeadStopMoving()
    {
        if(isDead)
        {
        isStopped = true;
        direction = Vector2.zero;
        foreach (var col in colliders)
        {
            col.enabled = false;
        }
        }
    }
    void Update()
    {
         
        if (isStopped || isDead) return;

        if (chasePlayer.enabled == false)
        {
           
            
            // Only handle random movement if ChasePlayer is disabled
            myAnimator.SetBool("isRunning", true);
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                StartCoroutine(StopBeforeMoving());
            }
           

            float distance = Vector2.Distance(transform.position, chasePlayer.chaseTarget.transform.position);
            if(distance < chasePlayer.distanceChase)
            {
               
                chasePlayer.enabled = true;
                this.enabled = false; // Disable this script
                
            }
        }

        FlipSprite();
        
    }

    IEnumerator InitializeMovement()
    {
        myAnimator.SetBool("isRunning", false);
        isStopped = true;
        direction = Vector2.zero;
        yield return new WaitForSeconds(1f);
        isStopped = false;
        direction = Random.insideUnitSphere;
        timer = Random.Range(minTime, maxTime);
        myAnimator.SetBool("isRunning", true);
    }
    
     IEnumerator StopBeforeMoving()
    {
        myAnimator.SetBool("isRunning", false);
        isStopped = true;
        direction = Vector2.zero;
        yield return new WaitForSeconds(1f); 
            myAnimator.SetBool("isRunning", true);
            direction = Random.insideUnitSphere;
            timer = Random.Range(minTime, maxTime);
            isStopped = false;  
    }

  

  void Freeze()
  {
    direction = Vector2.zero;
  }

  

     void FlipSprite()
    { 
     if(direction.x < 0 && transform.localScale.x > 0) 
        {
            Flip();
        } 
        else if(direction.x > 0 && transform.localScale.x < 0)
            {
                Flip();
               
            }
        
        
    }
   

   void Flip()
    {
        
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

   
    public void ResetTransform()
    {
        transform.localScale = initialScale;
        
    }
}
