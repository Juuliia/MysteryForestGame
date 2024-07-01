using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMessage : MonoBehaviour
{
   
   [SerializeField] private GameObject objectToAppear;
    [SerializeField] private GameObject objectToExist;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!objectToExist.activeSelf)
            {
                objectToAppear.SetActive(true);
                StartCoroutine(HideObject());
                // myAnimator.speed = 0f;
            }
            else
            {
                objectToAppear.SetActive(true);
                objectToExist.SetActive(false);
            }
        }
    }

    IEnumerator HideObject()
    {
        yield return new WaitForSeconds(5f);
        objectToAppear.SetActive(false);
    }
           
            
        
       
}
