using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioPool audioPool;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (audioPool == null)
        {
            audioPool = GetComponent<AudioPool>();

            if (audioPool == null)
            {
                Debug.LogError("AudioPool is not assigned and not found on the AudioManager.");
            }
        }
    }

    public void PlayAudioClip(AudioClip clip, float volume = 1f, float pitch = 1f, bool loop = false)
    {
        AudioSource audioSource = audioPool.GetAudioSource();

        if (audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.loop = loop;
            audioSource.Play();

            if (!loop)
            {
                audioPool.ReturnAudioSourceToPool(audioSource, audioSource.clip.length);
            }
        }
    }
}