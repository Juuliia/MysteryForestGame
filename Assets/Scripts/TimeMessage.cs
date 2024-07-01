using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMessage : MonoBehaviour
{
    [SerializeField] private GameObject objectToAppear;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                objectToAppear.SetActive(true);
                StartCoroutine(HideObject());

            }
            
        
    }

    IEnumerator HideObject()
    {
        yield return new WaitForSeconds(5f);
        objectToAppear.SetActive(false);
        Destroy(gameObject);
    }
           
            
}
