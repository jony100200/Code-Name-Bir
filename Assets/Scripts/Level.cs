using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
  void LoadStartMenu()
  {
      SceneManager.LoadScene(0);
  }

  public void LoadGame()
  {
      SceneManager.LoadScene("Level1");
  }

  public void LoadGameOver()
  {
      SceneManager.LoadScene("Game Over");
  }
}
