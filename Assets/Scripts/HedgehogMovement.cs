using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogMovement : MonoBehaviour
{
      
[SerializeField] float moveSpeed = 5f;
[SerializeField] float minTime = 1f;
[SerializeField] float maxTime = 5f;


Animator myAnimator;
Vector2 direction;
float timer;
bool isFacingRight;
bool isStopped = false;
Vector3 initialScale;


    void Start()
    {
        myAnimator = GetComponent<Animator>();
        initialScale = transform.localScale;
        timer = Random.Range(minTime, maxTime);
       
    }

    
    void Update()
    {   
        FlipSprite();
         
        if(gameObject.tag == "Peacful")
        {if(isStopped){return;}
            
        myAnimator.SetBool("isRunning", true);
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        timer -= Time.deltaTime;
         if(timer <= 0f){
        StartCoroutine(StopBeforeMoving());
        }
        }
        //StartCoroutine(RandomEvent());
    }

    IEnumerator StopBeforeMoving()
    {
        myAnimator.SetBool("isRunning", false);
        isStopped = true;
        direction = Vector2.zero;
        yield return new WaitForSeconds(2f); 
            myAnimator.SetBool("isRunning", true);
            direction = Random.insideUnitSphere;
            timer = Random.Range(minTime, maxTime);
            isStopped = false;  
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
}
