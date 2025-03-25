using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform Target;

    void Start()
{
    
}

    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(Target.position.x, Target.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
    }

}
