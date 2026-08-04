using UnityEngine;
using Core.Singleton;

public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp")]
    public Transform target;
    public float lerpspeed = 1f;
    public float speed = 1f;

    public string TagToCheckEnemy = "Enemy";
    public string TagToCheckEndLine = "EndLine";

    public GameObject Endscreen;

    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }


    void Update()
    {
        if (!_canRun) return;
        
        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, _pos, Time.deltaTime * lerpspeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == TagToCheckEnemy)
        {
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == TagToCheckEndLine)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        _canRun = false;
        Endscreen.SetActive(true);
    }

    public void StartRun()
    {
        _canRun = true;
    }

    #region PowerUps

    public void SetPowerUpText(string text)
    {
        //uiTextPowerUp.text = text;
    }

    public void PowerUpSpeedUp(float amount)
    {
        _currentSpeed = amount;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    #endregion
}
