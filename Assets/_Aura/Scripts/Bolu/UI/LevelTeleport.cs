using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelTeleport : MonoBehaviour
{
    public LoadingManager loadingManager;

    void OnTriggerEnter(Collider other)
    {
       loadingManager.Loadscene(2); 
    }


}
