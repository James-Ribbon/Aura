using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BloomPulse : MonoBehaviour
{
    public PostProcessVolume volume;


    [SerializeField] private float pulseSpeed = 1f;
    [SerializeField] float currentBloomValue = 0f;
    [SerializeField] private float maxBloomValue = 0.4f;

    private Bloom bloom;
    

    private void Start()
    {
        volume.profile.TryGetSettings(out bloom);

        if(bloom == null)
            enabled = false;
    }

    private void Update()
    {
        currentBloomValue = Mathf.PingPong(Time.time * pulseSpeed, 1f);

        float currentIntensity = Mathf.Lerp(0, maxBloomValue, currentBloomValue);

        bloom.intensity.value = currentIntensity;
    }
}
