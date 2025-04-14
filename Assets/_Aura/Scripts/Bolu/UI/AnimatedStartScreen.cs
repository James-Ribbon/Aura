
using UnityEngine;

public class AnimatedStartScreen : MonoBehaviour
{
   private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LightTrigger()
    {
        animator.SetBool("light", true);
    }
}
