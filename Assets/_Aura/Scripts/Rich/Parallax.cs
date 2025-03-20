using UnityEngine;

public class Parallax : MonoBehaviour
{
     Material mat;
    float distance;

    [Range(0f,0.2f)]
    public float speed=2.0f;
    public float dampen = 0.05f;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        distance += Time.deltaTime*speed * dampen;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}
