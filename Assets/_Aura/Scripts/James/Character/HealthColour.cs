using UnityEngine;

public class HealthColour : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float colorChangeSpeed = 2f; // How fast colors transition

    [Header("Color Thresholds")]
    public Color fullHealthColor = Color.green;
    public Color halfHealthColor = new Color(1f, 0.5f, 0f); // Orange
    public Color lowHealthColor = Color.red;
    public Color criticalHealthColor = Color.black;

    private LineRenderer _lineRenderer;
    private Color _targetColor;
    private Color _currentColor;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _currentColor = fullHealthColor;
        _lineRenderer.startColor = _currentColor;
        _lineRenderer.endColor = _currentColor;
    }

    void Update()
    {
        // Smoothly lerp toward the target color
        _currentColor = Color.Lerp(
            _currentColor,
            _targetColor,
            colorChangeSpeed * Time.deltaTime
        );

        _lineRenderer.startColor = _currentColor;
        _lineRenderer.endColor = _currentColor;
    }

    public void UpdateHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0f, maxHealth);
        _targetColor = GetHealthColor(currentHealth);
    }

    private Color GetHealthColor(float health)
    {
        float healthPercent = health / maxHealth;

        if (healthPercent <= 0.05f) return criticalHealthColor;
        if (healthPercent <= 0.2f) return Color.Lerp(criticalHealthColor, lowHealthColor, (healthPercent - 0.05f) / 0.15f);
        if (healthPercent <= 0.5f) return Color.Lerp(lowHealthColor, halfHealthColor, (healthPercent - 0.2f) / 0.3f);
        return Color.Lerp(halfHealthColor, fullHealthColor, (healthPercent - 0.5f) / 0.5f);
    }
}