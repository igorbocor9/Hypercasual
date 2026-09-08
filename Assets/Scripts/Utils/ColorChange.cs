using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    private float duration = 2f;
    public MeshRenderer meshRenderer;

    public Color startColor = Color.white;
    public Color _correctColor;

    private void OnValidate()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _correctColor = meshRenderer.materials[0].GetColor("_BaseColor");
        LerpColor();
    }

    private void LerpColor()
    {
        meshRenderer.materials[0].SetColor("_BaseColor", startColor);
        meshRenderer.materials[0].DOColor(_correctColor, duration).SetDelay(0.5f);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            LerpColor();
        }
    }
}
