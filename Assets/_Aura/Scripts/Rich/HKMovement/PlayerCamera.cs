using UnityEngine;

public class PlayerCamera : MonoBehaviour

{
    //Test
    float playerX;

    public bool IsFacingRight;

   private void TurnCheck()
   {
        playerX = Input.GetAxis("Horizontal"); //test
    if(playerX > 0 && !IsFacingRight)
    {
        Turn();
    }

    else if (playerX <0 && IsFacingRight)
    {
        Turn();
    }
   }

   private void Turn()
   {
    if (IsFacingRight)
    {
        Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
        transform.rotation = Quaternion.Euler(rotator);
        IsFacingRight = !IsFacingRight;

        //turn the camera follow object
        _cameraFollowObeject.CallTurn();
    }
    else
    {
        Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
        transform.rotation = Quaternion.Euler(rotator);
        IsFacingRight = !IsFacingRight;

         //turn the camera follow object
        _cameraFollowObeject.CallTurn();
    }
   }

private CameraFollowObject _cameraFollowObeject;

private float _fallSpeedYDampingChangeThreshold;
   
}
