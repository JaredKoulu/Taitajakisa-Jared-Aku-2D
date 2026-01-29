using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource audioSource;
    public AudioClip calmMusic;
    public AudioClip actionMusic; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayCalmMusic();
    }

    public void PlayCalmMusic()
    {
        audioSource.clip = calmMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayActionMusic()
    {
        audioSource.clip = actionMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}

