using UnityEngine;

public class SoulTether : MonoBehaviour
{
    [Header("Core Settings")]
    public Transform startPoint;  // Off-screen anchor (left)
    public Transform endPoint;    // Player
    public int segments = 20;     // More segments = smoother curve
    public float lineWidth = 0.15f;

    [Header("Wave Motion")]
    public float amplitude = 0.5f;  // How much it waves
    public float frequency = 1.5f;  // Wave speed
    public float waveLength = 2f;   // Distance between peaks

    [Header("Organic Noise")]
    public float noiseScale = 2f;   // Noise detail
    public float noiseSpeed = 1f;   // Noise movement speed
    public float noiseInfluence = 0.3f; // How much noise affects the wave

    [Header("Visual Effects")]
    public ParticleSystem soulParticles;
    public Gradient colorGradient;  // For ethereal glow
    public AnimationCurve widthCurve; // Dynamic thickness

    private LineRenderer lineRenderer;
    private float timeCounter;
    private float noiseOffset;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments;
        lineRenderer.widthCurve = widthCurve;
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.colorGradient = colorGradient;

        // Randomize noise offset for uniqueness
        noiseOffset = Random.Range(0f, 100f);

        // Default width curve if not set
        if (widthCurve.length == 0)
        {
            widthCurve = new AnimationCurve(
                new Keyframe(0, 0.1f),
                new Keyframe(0.3f, 0.3f),
                new Keyframe(0.7f, 0.3f),
                new Keyframe(1, 0.1f)
            );
        }
    }

    void Update()
    {
        timeCounter += Time.deltaTime * frequency;
        noiseOffset += Time.deltaTime * noiseSpeed;

        Vector3 startPos = startPoint.position;
        Vector3 endPos = endPoint.position;
        Vector3 direction = (endPos - startPos).normalized;
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward).normalized;

        float totalDistance = Vector3.Distance(startPos, endPos);

        // Update line positions
        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            Vector3 basePosition = Vector3.Lerp(startPos, endPos, t);

            // Sine wave motion (for smooth oscillations)
            float sinePhase = t * totalDistance / waveLength + timeCounter;
            float sineWave = Mathf.Sin(sinePhase * Mathf.PI * 2) * amplitude;

            // Perlin noise (for organic randomness)
            float noise = Mathf.PerlinNoise(t * noiseScale, noiseOffset);
            noise = (noise - 0.5f) * 2f * amplitude * noiseInfluence;

            // Combined effect
            float combinedOffset = sineWave + noise;

            lineRenderer.SetPosition(i, basePosition + perpendicular * combinedOffset);
        }

        // Add floating soul particles
        EmitParticlesAlongTether();
    }

    void EmitParticlesAlongTether()
    {
        if (soulParticles == null) return;

        soulParticles.Clear();

        for (int i = 0; i < segments - 1; i++)
        {
            Vector3 pos = lineRenderer.GetPosition(i);
            Vector3 nextPos = lineRenderer.GetPosition(i + 1);
            Vector3 midPoint = Vector3.Lerp(pos, nextPos, 0.5f);

            // Random emission for a natural look
            if (Random.value > 0.6f)
            {
                var emitParams = new ParticleSystem.EmitParams
                {
                    position = midPoint,
                    startSize = Random.Range(0.05f, 0.15f),
                    startColor = colorGradient.Evaluate(Random.value)
                };
                soulParticles.Emit(emitParams, 1);
            }
        }
    }
}