using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private PlayerScript player;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool isOpen = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }
    void Update()
    {
        DoorOpen();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isOpen)
        {
            player.transform.position = new Vector2(0,0);
            player.hasKey = false;
            player.storedCoins = player.coins;
            //SceneManager.LoadScene("VictoryScreen"); Test if end works Note to self spell VictoryScreen right....
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private void DoorOpen()
    {
        if (player.hasKey)
        {
            spriteRenderer.sprite = sprites[1];
            isOpen = true;
        }
        else
        {
            spriteRenderer.sprite = sprites[0];
            isOpen = false;
        }
    }
}
