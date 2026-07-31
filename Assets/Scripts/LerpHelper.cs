using UnityEngine;

public class LerpHelper : MonoBehaviour
{
    public Transform target;
    public float lerpspeed = 1f;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * lerpspeed);
    }
}
