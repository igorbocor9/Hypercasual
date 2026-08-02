using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //publics
    [Header("Lerp")]
    public Transform target;
    public float lerpspeed = 1f;
    public float speed = 1f;

    public string compareTag = "Enemy";

    //privates
    private bool _canRun;
    private Vector3 _pos;

    private void Start()
    {
        _canRun = true;
    }

    void Update()
    {
        if (!_canRun) return;
        
        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.Translate(transform.forward * speed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, _pos, Time.deltaTime * lerpspeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == compareTag)
        {
            _canRun = false;
        }
    }
}
