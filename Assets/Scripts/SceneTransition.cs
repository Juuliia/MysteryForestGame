using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 newPlayerPosition;
   public Vector2 returnPlayerPosition;
[SerializeField] private Vector2 defaultStartPosition;
void Start()
    {
       // LevelManager.instance.SetStartPosition(defaultStartPosition);
    }

     void OnTriggerEnter2D(Collider2D other)
    {
      
        if (other.CompareTag("Player"))
        {
            LevelManager levelManager = LevelManager.instance;
            if (levelManager != null)
            {
                levelManager.SavePlayerPosition(returnPlayerPosition);
                Debug.Log("Player position saved for scene transition: " + returnPlayerPosition);
            }
            else
            {
                Debug.LogError("LevelManager instance is null in SceneTransition!");
            }

            SceneManager.LoadScene(sceneToLoad);
        }
    

    
}
}
