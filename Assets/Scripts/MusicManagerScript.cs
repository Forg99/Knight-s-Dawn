using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagerScript : MonoBehaviour
{
    [Header("Playlist")]
    public AudioClip[] tracks;

    [Header("Settings")]
    public bool shuffle = false;
    public bool loopPlaylist = true;

    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    private static MusicManagerScript instance;

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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (tracks.Length > 0)
        {
            PlayTrack(currentTrackIndex);
        }
    }

        void Update()
    {
            if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "VicotoryScreen")
        {
            Destroy(gameObject);
        }

        // When current track finishes
        if (!audioSource.isPlaying)
        {
            NextTrack();
        }
    }

    void PlayTrack(int index)
    {
        if (tracks.Length == 0) return;

        audioSource.clip = tracks[index];
        audioSource.Play();

        Debug.Log("Now Playing: " + tracks[index].name);
    }

    public void NextTrack()
    {
        if (tracks.Length == 0) return;

        if (shuffle)
        {
            currentTrackIndex = Random.Range(0, tracks.Length);
        }
        else
        {
            currentTrackIndex++;

            if (currentTrackIndex >= tracks.Length)
            {
                if (loopPlaylist)
                {
                    currentTrackIndex = 0;
                }
                else
                {
                    return;
                }
            }
        }

        PlayTrack(currentTrackIndex);
    }
}
