using System;
using UnityEngine;
using static UnityEngine.CullingGroup;



// Dynamic Music States
public enum GameMusic {
    startMusic,
    dead01,
    dead02,
    BossCrab,
    BossParrot,
    spaceMusic,
    PirateMusic,
    endingMusic,
}

// In charged of playing music as per action called
public class MusicManager : MonoBehaviour
{
    [Header("Music Background")]
    public static int currentSong = 0;
    public AudioClip[] backgroundMusic;
    [SerializeField] public AudioSource audioSource;

    GameMusic music;
    public static event Action<GameMusic> OnMusicChanged;
    public static MusicManager instance { get; private set; }



    //MUSIC  
    public void currentSongPlaying()
    {
        Debug.Log(backgroundMusic[currentSong].name);
        return;
    }

    public void Start()
    {
        UpdateGameMusic(GameMusic.startMusic);

    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }


    public void UpdateGameMusic(GameMusic newMusic)
    {
        music = newMusic;

        switch (newMusic)
        {
            case GameMusic.startMusic:
                HandleStartMusic();
                break;
            case GameMusic.dead01:
                break;
            case GameMusic.dead02:
                break;
            case GameMusic.BossCrab:
                HandleFight01Music();
                break;
            case GameMusic.BossParrot:
                break;
            case GameMusic.spaceMusic:
                HandleStartMusic();
                break;
            case GameMusic.PirateMusic:
                break;
            case GameMusic.endingMusic:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newMusic), newMusic, null);
        }

        OnMusicChanged?.Invoke(newMusic);

    }

    public void HandleStartMusic()
    {
        changeSong(0);

    }

    public void HandleFight01Music()
    {
        changeSong(1);

    }


    public void changeSong(int songID)
    {
        currentSong = songID;
        audioSource.clip = backgroundMusic[songID];
        audioSource.Play();
        currentSongPlaying();
    }

    public void PirateShipSong() //Test
    {
        changeSong(1);
    }

    public void SpaceShipSong() //Test
    {
        changeSong(0);
    }


}
