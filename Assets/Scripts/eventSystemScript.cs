using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class eventSystemScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
      private static eventSystemScript instance;

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
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(gameObject);
        }
    }
}
