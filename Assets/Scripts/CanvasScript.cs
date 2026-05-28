using UnityEngine;
using UnityEngine.SceneManagement;


public class CanvasScript : MonoBehaviour
{
      private static CanvasScript instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "VictoryScreen")
        {
            Destroy(gameObject);
        }
    }

}
