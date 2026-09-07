using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Core.Singleton;
using System.Linq;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    public List<ItemCollectableCoin> itens;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        itens = new List<ItemCollectableCoin>();
    }

    public void RegisterCoin(ItemCollectableCoin coin)
    {
        if (!itens.Contains(coin))
        {
            itens.Add(coin);
            coin.transform.localScale = Vector3.zero;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartAnimation();
        }
    }

    public void StartAnimation()
    {
        StartCoroutine(ScalePiecesByTime());
    }

    private IEnumerator ScalePiecesByTime()
    {
        foreach(var p in itens)
        {
            p.transform.localScale = Vector3.zero;
        }

        sort();

        yield return null;

        for (int i = 0; i < itens.Count; i++)
        {
            itens[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }
    }

    private void sort()
    {
        itens = itens.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}
