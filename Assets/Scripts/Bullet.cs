using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField] float bulletSpeed;
    Vector2 direction;
    AudioPlayer audioPlayer;


    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        audioPlayer.PlayShootinClip();
    }

    
    void Update()
    {
      
        myRigidbody.velocity = direction * bulletSpeed;
        
    }

   public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }
    
}
