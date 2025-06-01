using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool allNotesCollected = false;  // T�m notlar topland� m�?

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Final sahnesine gelince kontrol et
        if (scene.name == "FinalScene")
        {
            if (allNotesCollected)
                LoadEndScene();  // Oyunu kazand�n
            else
                LoadLoseScene(); // T�m notlar� toplamad�n
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadKMFScene()
    {
        SceneManager.LoadScene("KMFScene");
    }

    public void LoadFinalScene()
    {
        SceneManager.LoadScene("FinalScene");
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void LoadLoseScene()
    {
        SceneManager.LoadScene("LoseScene");  // Bu sahneyi olu�turman gerek
    }
}
