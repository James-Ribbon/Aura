using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    public Transform player;
    public Ability[] abilities;

    private void Update()
    {
        transform.position = player.transform.position;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetMouseButtonDown(1))
        {
            // FIX: Hard Coded, will fix later

            abilities[0].ActivateAbility();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            abilities[0].DeactivateAbility();
        }
    }

    


}
