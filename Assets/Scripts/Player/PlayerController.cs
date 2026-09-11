using UnityEngine;
using Core.Singleton;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
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


    [Header("Coin setup")]
    public GameObject coinCollector;

    [Header("TextMeshPro")]
    public TextMeshPro uiTextPowerUp;

    [Header("Animation")]
    public AnimatorManager animatorManager;
    public float scaleDuration = 1f;
    public float scaleBounce = 1f;
    public Ease ease = Ease.OutBack;

    [Header("VFX")]
    public ParticleSystem VFXDeath;

    [Header("Limits")]
    public float limit = 4;
    public Vector2 limitVector = new Vector2(-4, 4);

    [SerializeField] private BounceHelper _bounceHelper;

    public bool invincible = false;

    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 7f;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        _startPosition = transform.position;
        ResetSpeed();
    }

    public void Bounce()
    {
        if (_bounceHelper != null)
        {
            _bounceHelper.Bounce();
        }
    }

    void Update()
    {
        if (!_canRun) return;
        
        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        if (_pos.x < limitVector.x)
        {
            _pos.x = limitVector.x;
        }
        else if (_pos.x > limitVector.y)
        {
            _pos.x = limitVector.y;
        }

        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, _pos, Time.deltaTime * lerpspeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == TagToCheckEnemy)
        {
            if (!invincible)
            {
                MoveBack();
                EndGame(AnimatorManager.AnimatorType.DEAD);
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

    private void MoveBack()
    {
        transform.DOMoveZ(-1f, 0.5f).SetRelative();
    }

    private void EndGame(AnimatorManager.AnimatorType type = AnimatorManager.AnimatorType.IDLE)
    {
        _canRun = false;
        Endscreen.SetActive(true);
        animatorManager.Play(type);
        if (VFXDeath != null)
        {
            VFXDeath.Play();
        }
    }

    public void StartRun()
    {
        transform.DOScale(scaleBounce, scaleDuration).SetEase(ease);
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimatorType.RUN, _currentSpeed / _baseSpeedToAnimation);
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

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }

    #endregion
}
