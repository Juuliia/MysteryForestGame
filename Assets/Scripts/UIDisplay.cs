using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIDisplay : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;
    [SerializeField] GameObject flipTarget;
    bool isFacingRight;
    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
        isFacingRight = flipTarget.transform.localScale.x > 0;
    }

    
    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        FlipSlider();
    }

 
    void FlipSlider()
    {
        if(flipTarget != null)
        {
            bool currentFacingRight = flipTarget.transform.localScale.x > 0;
       if (currentFacingRight != isFacingRight)
            {
                // Flip the slider
                Flip();
            }

            // Update isFacingRight to current direction
            isFacingRight = currentFacingRight;
        }
            
    }
    
     void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = healthSlider.transform.localScale;
        localScale.x *= -1;
        healthSlider.transform.localScale = localScale;
    }

}
