
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SceneTransition
{
    public class LoadingManager : MonoBehaviour
    {
        public static LoadingManager instance;
        
        public int transitionSceneIndex;
        public float fadeSpeed = 0.5f;
        public float zoomSpeed = 0.5f;
        public Image fadeImg;
        
        private int startSceneIndex;
        private int endSceneIndex;
        
        private Coroutine transitionCoroutine;

        public UnityEvent OnLoadComplete = new UnityEvent();
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        public void LoadScene(int sceneIndex)
        {
            startSceneIndex = SceneManager.GetActiveScene().buildIndex;
            endSceneIndex = sceneIndex;
            transitionCoroutine = StartCoroutine(PerformTransition());
        }

        IEnumerator PerformTransition()
        {
            //first fade in 
            Color imgCol = Color.white;
            imgCol.a = 0;
            float lerpValue = 0;
            
            while (imgCol.a < 0.99f)
            {
                imgCol.a = Mathf.Lerp(0.0f, 1.0f,lerpValue * fadeSpeed);
                fadeImg.color = imgCol;
                lerpValue += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            fadeImg.color = Color.white;
            
            //LoadTransition
            SceneManager.LoadScene(transitionSceneIndex);
            
            lerpValue = 0;
            
            while (imgCol.a > 0.01f)
            {
                imgCol.a = Mathf.Lerp(1.0f, 0.0f,lerpValue * fadeSpeed * 2);
                fadeImg.color = imgCol;
                lerpValue += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            fadeImg.color = Color.clear;
            
            //load main scene in background.
            var transitionScene = SceneManager.GetActiveScene();
            var asyncOper = SceneManager.LoadSceneAsync(endSceneIndex);
            
            if (asyncOper != null)
            {
                asyncOper.allowSceneActivation = false;

                while (asyncOper?.progress < 0.9f)
                {
                    yield return new WaitForEndOfFrame();
                }

                yield return new WaitForSeconds(2);
                
                //Start Camera Animation
                OnLoadComplete?.Invoke();
                yield return new WaitForSeconds(zoomSpeed);

                yield return new WaitForSeconds(2);
                
                asyncOper.allowSceneActivation = true;
                SceneManager.UnloadSceneAsync(transitionScene);
                
            }
            
            

        }
        
    }
}
