using UnityEngine;

public class Floater : Enemy
{
    /*
    -- Enemy floats around world area as if random
    -- If player gets too close it will track the player and "attack"
    -- Attack would be a tendril moving towards the player/contact with player
     */

    [Header("A Transform for the enemy to float around")]
    public Transform originPoint;

    public override void Init()
    {
        base.Init();
    }
}
