using UnityEngine;
using UnityEngine.SceneManagement;

public class AudiManagerScript : MonoBehaviour
{
      private static AudiManagerScript instance;

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
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "2ndLvl")
        {
            Destroy(gameObject);
        }
    }
}
