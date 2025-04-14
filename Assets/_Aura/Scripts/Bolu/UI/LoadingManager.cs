
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private string levelName;
    void Start()
    {
        levelName = "ThirdPersonGame";
    }
    public void Loadscene(int index)
    {


        SceneManager.LoadScene(index);
    }

    public void LoadLevel(){
        SceneManager.LoadScene(levelName);
    }
}
