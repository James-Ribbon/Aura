using UnityEngine;

public class ShieldAbility : Ability
{
    private GameObject shieldObject;

    public Animator Animator;

    public Knockback knockback;

    private void Awake()
    {
        shieldObject = gameObject;

        DeactivateAbility();
    }

    public override void ActivateAbility()
    {
        base.ActivateAbility();
        Animator.SetBool("ShieldActive", true);
        shieldObject.SetActive(true);
        knockback.Activate();
    }

    public override void DeactivateAbility()
    {
        base.DeactivateAbility();
        Animator.SetBool("ShieldActive", false);

        shieldObject.SetActive(false);
    }
}

