using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    Health health;
    [SerializeField] int addHealth = 10;
     [Header("SoundEffect")]
    [SerializeField] AudioClip soundEffectClip;
    [SerializeField] [Range(0f, 1f)] float soundgVolume = 1f;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
       if(other.gameObject.tag == "Player")
       {
        Health health = other.GetComponent<Health>();
            if (health != null)
            {
                 if(soundEffectClip != null)
    {
        AudioSource.PlayClipAtPoint(soundEffectClip, 
        Camera.main.transform.position, soundgVolume);
    }
                health.AddHealth(addHealth);
                Destroy(gameObject); // Destroy the food once health is added
            }
        }
       }

    }

   
    

