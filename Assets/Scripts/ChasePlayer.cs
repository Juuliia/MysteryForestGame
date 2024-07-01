using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
   [SerializeField]public  GameObject chaseTarget;
    [SerializeField] float chaseSpeed;
    [SerializeField] public float distanceChase = 5f;
    public float distance;
    EnemyMovement enemyMovement;
    Vector2 direction;
    Animator myAnimator;
   PlayerMovement playerMovement;
    
   Vector3 initialScale;
   
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
       initialScale = transform.localScale;
        playerMovement = GetComponent<PlayerMovement>();
      
    }


   void OnTriggerEnter2D(Collider2D collider)
   {
    if(gameObject.tag == "Player"){
    myAnimator.SetTrigger("isAttacking");}
   }
   
    void Update()
    {    
       
     
       distance = Vector2.Distance(transform.position, chaseTarget.transform.position);

        if (distance < distanceChase)
        {
            
            // Chase the player
            Vector2 direction = chaseTarget.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, chaseTarget.transform.position, chaseSpeed * Time.deltaTime);
            myAnimator.SetBool("isRunning", true);
            enemyMovement.enabled = false;

            // Flip the enemy based on the direction
            
        if(direction.x < 0 && transform.localScale.x > 0) 
        {
            Flip();
        } 
        else if(direction.x > 0 && transform.localScale.x < 0)
            {
                Flip();
               
            }
        
   
            
           
        }
        else
        {
           
            
            // Switch to random movement
            enemyMovement.enabled = true;
            myAnimator.SetBool("isRunning", false);
            this.enabled = false; // Disable this script
            
            
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


    
   

  
