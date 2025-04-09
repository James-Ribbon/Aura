using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObjects;

    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        //customInspectorObjects = new CustomInspectorObjects();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            /*if (customInspectorObjects.swapCamera)
            {
                SwapCamera();
            }*/
            if (customInspectorObjects.panCameraOnContact)
            {
                CameraManager.Instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (customInspectorObjects.swapCamera && customInspectorObjects.cameraLeft != null && customInspectorObjects.cameraRight != null)
            {
                Vector2 exitDirection = (other.transform.position - _collider.bounds.center).normalized;
                Debug.Log("Exit Direction: " + exitDirection);
                CameraManager.Instance.SwapCamera(customInspectorObjects.cameraLeft, customInspectorObjects.cameraRight, exitDirection);
            }

            if (customInspectorObjects.panCameraOnContact)
            {
                CameraManager.Instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, true);

            }
        }
    }

}

[System.Serializable]
public class CustomInspectorObjects
{
    public bool swapCamera = false;
    public bool panCameraOnContact = false;

    public CinemachineVirtualCamera cameraLeft;
    public CinemachineVirtualCamera cameraRight;

    public PanDirection panDirection;
    public float panDistance = 3f;
    public float panTime = 0.35f;
}

public enum PanDirection
{
    Left,
    Right,
    Up,
    Down
}

/*[CustomEditor(typeof(CameraControlTrigger))]
public class CameraControlTriggerEditor : Editor
{
    CameraControllTrigger cameraControllTrigger;

    private void OnEnable()
    {
        cameraControllTrigger = (CameraControllTrigger)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CustomInspectorObjects customInspectorObjects = new CustomInspectorObjects();
        customInspectorObjects.swapCamera = EditorGUILayout.Toggle("Swap Camera", customInspectorObjects.swapCamera);
        customInspectorObjects.panCameraOnContact = EditorGUILayout.Toggle("Pan Camera on Contact", customInspectorObjects.panCameraOnContact);

        if (customInspectorObjects.swapCamera)
        {
            customInspectorObjects.cameraLeft = (CinemachineVirtualCamera)EditorGUILayout.ObjectField("Left Camera", customInspectorObjects.cameraLeft, typeof(CinemachineVirtualCamera), true);
            customInspectorObjects.cameraRight = (CinemachineVirtualCamera)EditorGUILayout.ObjectField("Right Camera", customInspectorObjects.cameraRight, typeof(CinemachineVirtualCamera), true);
        }
        if (customInspectorObjects.panCameraOnContact)
        {
            customInspectorObjects.panDirection = (PanDirection)EditorGUILayout.EnumPopup("Pan Direction", customInspectorObjects.panDirection);
            customInspectorObjects.panDistance = EditorGUILayout.FloatField("Pan Distance", customInspectorObjects.panDistance);
            customInspectorObjects.panTime = EditorGUILayout.FloatField("Pan Time", customInspectorObjects.panTime);
        }
    }
}*/