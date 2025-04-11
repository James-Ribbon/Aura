using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewParallax : MonoBehaviour
{
    public Camera targetCamera;

    [Header("Parallax factor (0 = no movement, 1 = moves with camera)")]
    [Range(0f, 1f)]
    public float parallaxFactor = 0.5f;

    public bool verticalParallax = false;

    private Vector3 _lastCameraPosition;
    private float _textureUnitSizeX;
    private float _textureUnitSizeY;

    private void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        _lastCameraPosition = targetCamera.transform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;

        //Texture size
        _textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        _textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    private void FixedUpdate()
    {
        Vector3 cameraPosition = targetCamera.transform.position;
        Vector3 deltaMovement = cameraPosition - _lastCameraPosition;

        // Apply parallax effect
        Vector3 parallaxMovement = new Vector3(
            deltaMovement.x * parallaxFactor,
            verticalParallax ? deltaMovement.y * parallaxFactor : 0f,
            0f);

        transform.position += parallaxMovement;
        _lastCameraPosition = cameraPosition;

        //Horizontal scrolling
        if (Mathf.Abs(cameraPosition.x - transform.position.x) >= _textureUnitSizeX)
        {
            float offsetPositionX = (cameraPosition.x - transform.position.x) % _textureUnitSizeX;

            transform.position = new Vector3(cameraPosition.x + offsetPositionX, transform.position.y, transform.position.z);
        }

        //Vertical scrolling
        if (verticalParallax && Mathf.Abs(cameraPosition.y - transform.position.y) >= _textureUnitSizeY)
        {
            float offsetPositionY = (cameraPosition.y - transform.position.y) % _textureUnitSizeY;

            transform.position = new Vector3(transform.position.x, cameraPosition.y + offsetPositionY, transform.position.z);
        }
    }
}
