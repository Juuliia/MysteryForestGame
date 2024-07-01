using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSessionManager : MonoBehaviour
{
    [SerializeField] Slider sliderHealth;
    [SerializeField] Health playerHealth;


   void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (playerHealth == null)
        {
            Debug.LogError("Player Health is not assigned in GameSessionManager.");
        }
    }

    public void SaveGame()
    {
        if (playerHealth != null)
        {
            playerHealth.SaveHealth();
        }
        else
        {
            Debug.LogError("Player Health is null in SaveGame().");
        }
    }
}