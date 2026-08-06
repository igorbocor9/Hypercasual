using UnityEngine;
using Core.Singleton;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

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

    [Header("TextMeshPro")]
    public TextMeshPro uiTextPowerUp;

    public bool invincible = false;

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
            if (!invincible)
            {
                EndGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == TagToCheckEndLine)
        {
            if (!invincible)
            {
                EndGame();
            }
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
        uiTextPowerUp.text = text;
    }

    public void PowerUpSpeedUp(float amount)
    {
        _currentSpeed = amount;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvincible(bool value = true)
    {
        invincible = value;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        /*var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight(float animationDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y, animationDuration).SetEase(ease);
    }

    #endregion
}
