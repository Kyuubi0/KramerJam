using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Start"); // Ya da kendi sahne adýn
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting..."); // Sadece Editor'da görünür
    }
}