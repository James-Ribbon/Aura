using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

namespace SceneTransition
{
    public class LoadingZoom : MonoBehaviour
    {
        public float zoomSpeed = 2;
        public bool onAwake = false;
        public float targetZoom;

        private Camera cam;
        private Coroutine zoomCoroutine;
        
        
        
        private void Awake()
        {
            cam = Camera.main;
            if (onAwake)
            {
                zoomCoroutine = StartCoroutine(StartZoomEffect());
            }
            else
            {
                LoadingManager.instance.OnLoadComplete.AddListener(() =>
                {
                    zoomCoroutine = StartCoroutine(StartZoomEffect());
                });
            }
        }

        IEnumerator StartZoomEffect()
        {
            float start = cam.orthographicSize;

            float lerpVal = 0;

            while (!Mathf.Approximately(cam.orthographicSize, targetZoom))
            {
                cam.orthographicSize = Mathf.Lerp(start, targetZoom, lerpVal * zoomSpeed );
                lerpVal += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
