using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected bool activateAbility = false;

    public virtual void ActivateAbility()
    {
        activateAbility = true;
    }

    public virtual void DeactivateAbility()
    {
        activateAbility = false;
    }
}
