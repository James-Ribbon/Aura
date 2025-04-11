using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : ObjectPool
{
    //[SerializeField] private AudioSource audioSourcePrefab;

    protected override void Start()
    {
        //objectToPool = audioSourcePrefab.gameObject;

        base.Start();
    }

    public AudioSource GetAudioSource()
    {
        GameObject obj = GetPooledObject();

        if (obj != null)
        {
            AudioSource audioSource = obj.GetComponent<AudioSource>();

            if (audioSource != null)
            {
                audioSource.gameObject.SetActive(true);
                return audioSource;
            }
        }
        return null;
    }

    public void ReturnAudioSourceToPool(AudioSource source, float duration)
    {
        StartCoroutine(ReturnToPoolAfterPlaying(source, duration));
    }

    private IEnumerator ReturnToPoolAfterPlaying(AudioSource source, float duration)
    {
        yield return new WaitForSeconds(duration);
        ReturnAudioSource(source);
    }

    public void ReturnAudioSource(AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.clip = null;
        ReturnToPool(audioSource.gameObject);
    }
}