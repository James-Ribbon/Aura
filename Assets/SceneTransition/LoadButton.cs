
using UnityEngine.UI;
using UnityEngine;

namespace SceneTransition
{
    public class LoadButton : MonoBehaviour
    {
        public int sceneToLoad;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => LoadScene(sceneToLoad));
        }

        public void LoadScene(int index)
        {
            LoadingManager.instance.LoadScene(index);   
        }
    }
}
