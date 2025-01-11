using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioSource> gameAudios = new List<AudioSource>();

    public enum GameAudioSource
    {
        PlayerTakeDamageAudio,
        PlayerShootAudio,
        EnemyAudio
    }

    private static AudioManager _instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<AudioManager>();
            return _instance;
        }
    }
    private AudioManager() { }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        Initialize();
    }

    public void Initialize()
    {

    }

    public AudioSource GetAudioSource(GameAudioSource audioSource)
    {
        return gameAudios[(int)audioSource];
    }

    public void PlayerAudioOneShot(GameAudioSource audioSource,AudioClip clip)
    {
        gameAudios[(int)audioSource].PlayOneShot(clip);
    }
}
