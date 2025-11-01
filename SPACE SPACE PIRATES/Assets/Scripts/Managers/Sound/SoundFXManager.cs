using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
   
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume) {
        //Spawn The GO
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        // assign the audioclip
        audioSource.clip = audioClip;
        //assign Volume
        audioSource.volume = volume;    
        //Play Sound
        audioSource.Play();
        //get Lenght of sound clip
        float clipLenght = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLenght);
    
    
    }

    public void PlaySoundFXClipArray(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
         int random = Random.Range(0, audioClip.Length);


        //Spawn The GO
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        // assign the audioclip
        audioSource.clip = audioClip[random];
        //assign Volume
        audioSource.volume = volume;
        //Play Sound
        audioSource.Play();
        //get Lenght of sound clip
        float clipLenght = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLenght);


    }

}
