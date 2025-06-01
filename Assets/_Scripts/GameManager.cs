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
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadFinalScene()
    {
        SceneManager.LoadScene("KMFScene");
    }

    // �stersen ba�ka y�netim fonksiyonlar� ekleyebilirsin.
}
