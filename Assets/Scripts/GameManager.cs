using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    
    public static GameManager Instance { get; private set; }
    [SerializeField] private PlayerScript player;
    [SerializeField] private Text coinText;
    [SerializeField] private Text deathText;
    private Text victoryDeaths;
    private Text victoryCoins;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject doubleJumpPic;
    [SerializeField] private DoorScript door;
    public int coins;
    public int deaths;
   // int LayerInvincibility = LayerMask.NameToLayer("invincibility");
   // int LayerUndoInvincibility = LayerMask.NameToLayer("Default");
    

    private void Awake()
    {
        if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    else if (Instance != this)
    {
        Destroy(gameObject);
    }
    
        
        
    }
    
        private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        
        
        gameOverUI.SetActive(false);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject vc = GameObject.Find("VictoryCoins");
        GameObject vd = GameObject.Find("VictoryDeaths");

        if (vc != null)
            victoryCoins = vc.GetComponent<Text>();

        if (vd != null)
            victoryDeaths = vd.GetComponent<Text>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" )
        {
            Destroy(gameObject);
        }
        coins = player.coins;
        deaths = player.deaths;
        
        if (gameOverUI.activeInHierarchy && Input.GetKeyDown(KeyCode.Tab))
        {
            Respawn();
        }

        SetCoins();
        SetDeaths();
        //SetVictoryDeaths();
        //SetVictoryCoins();
        SetDoubleJump();

    }
    private void SetDoubleJump()
    {
        if (player.isAllowedToJump)
        {
            doubleJumpPic.SetActive(true);
        }
        else
        {
            doubleJumpPic.SetActive(false);
        }
    }
    private void SetCoins()
    {
       // player.coins = coins;
        coinText.text = "x" + player.coins.ToString();
    }
        private void SetDeaths()
    {
        //player.deaths = deaths;
        deathText.text = "x" + player.deaths.ToString();
    }
   /* private void SetVictoryDeaths()
    {
        if (victoryDeaths != null)
        {
            victoryDeaths.text = "x" + player.deaths.ToString();
        }
    }

    private void SetVictoryCoins()
    {
        if (victoryCoins != null)
        {
            victoryCoins.text = "x" + player.coins.ToString();
        }
    }*/

        public void Respawn()
    {
        player.animator.SetTrigger("Respawn");
        player.transform.position = Vector3.zero;
       // player.gameObject.layer = LayerUndoInvincibility;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.canMove = true;
        
        gameOverUI.SetActive(false);
    }

        public void OnPlayerDeath(PlayerScript player)
    {
         if (gameOverUI.activeSelf)
        return;

        player.animator.SetTrigger("Death");
       // player.gameObject.layer = LayerInvincibility;
        player.coins = player.storedCoins;
        player.deaths++;
        player.hasKey = false;
        player.isAllowedToJump = false;
        player.canMove = false;
        gameOverUI.SetActive(true);
        
        
    }

    public void onExit()
    {
        Application.Quit();
    }
    public void onPlay()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(1);
    }
    public void backToMainMenu()
    {
        SceneManager.LoadScene(0);
        gameOverUI.SetActive(false);
        player.transform.position = new Vector2(100 , 100);
    }
}
