using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    void Start()
    {
        // Check if LevelManager instance is not null
        if (LevelManager.instance != null)
        {
            // Set the player's position to the saved position
            transform.position = LevelManager.instance.LoadPlayerPosition();
        }
    }
}
