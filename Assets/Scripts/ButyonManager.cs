using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButyonManager : MonoBehaviour
{
    public void LoadGame()
   {
    SceneManager.LoadScene("Level1");
   }

   public void LoadMenu()
   {
    SceneManager.LoadScene("PlayMenu");
   }

   public void LoadGameOver()
   {
    SceneManager.LoadScene("GameOver");
   }

   public void LoadInstr()
   {
    SceneManager.LoadScene("Instr");
   }

   public void Quit()
   {
    Debug.Log("Quitting Game...");
    Application.Quit();
   }
}
