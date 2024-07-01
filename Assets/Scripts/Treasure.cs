using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Treasure : MonoBehaviour
{
    Animator myAnimator;
       [Header("SoundEffect")]
    [SerializeField] AudioClip soundEffectClip1;
    [SerializeField] AudioClip soundEffectClip2;
    [SerializeField] [Range(0f, 1f)] float soundgVolume1 = 1f;
     [SerializeField] [Range(0f, 1f)] float soundgVolume2 = 1f;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        myAnimator.SetTrigger("isOpen");
          if(soundEffectClip1 != null)
    {
        AudioSource.PlayClipAtPoint(soundEffectClip1, 
        Camera.main.transform.position, soundgVolume1);
    }

         if(soundEffectClip2 != null)
    {
        AudioSource.PlayClipAtPoint(soundEffectClip2, 
        Camera.main.transform.position, soundgVolume2);
    }
        StartCoroutine(WaitBeforeLoad());

    }
    
    IEnumerator WaitBeforeLoad()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Win");
    }
}   
