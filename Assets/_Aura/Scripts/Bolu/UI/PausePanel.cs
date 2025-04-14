
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {

            animator.SetTrigger("Toggle");
        }
    }
}
