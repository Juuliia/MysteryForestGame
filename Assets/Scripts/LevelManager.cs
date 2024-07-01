using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Vector2 playerPosition;
    public Vector2 startPosition;

     void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("LevelManager instance created and set to not destroy on load.");
        }
        else
        {
            Debug.LogWarning("Another instance of LevelManager found and destroyed.");
            Destroy(gameObject);
        }
    }

    public void SavePlayerPosition(Vector2 position)
    {
        playerPosition = position;
        Debug.Log("Player position saved: " + playerPosition);
    }

    public Vector2 LoadPlayerPosition()
    {
        Debug.Log("Player position loaded: " + playerPosition);
        return playerPosition;
    }

    public void SetStartPosition(Vector2 position)
    {
        startPosition = position;
        Debug.Log("Start position set: " + startPosition);
    }

    public Vector2 GetStartPosition()
    {
        return startPosition;
    }
  
}
