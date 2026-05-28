using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;
     private static CameraScript instance;

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
            if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "VicotoryScreen")
        {
            Destroy(gameObject);
        }

    }
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp
        (transform.position,desiredPosition,smoothSpeed * Time.deltaTime);
    }
}