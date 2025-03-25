using UnityEngine;

public class VertMove : MonoBehaviour {

    public float ms = 6;

    // update is called once per frame

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * ms * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * ms * Time.deltaTime);
        }
    }
}