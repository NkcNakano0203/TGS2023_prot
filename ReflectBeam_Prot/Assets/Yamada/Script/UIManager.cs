using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] RestartCounter restartCounter;

    [SerializeField] Image item = null;
    [SerializeField] TextMeshProUGUI timeCountText = null;
    [SerializeField] TextMeshProUGUI restartCountText = null;

    [SerializeField] Image backGround;

    [SerializeField] Ease ease;

    [SerializeField] Sprite ItemSprite;

    void Start()
    {
        gm.ClearProp.Where(x => x).Subscribe(x => OnClear());
    }

    void Update()
    {

    }

    void OnClear()
    {
        if (gm.GetItem)
        {
            item.sprite = ItemSprite;
        }

        timeCountText.text = $"クリアタイム {gm.gameTime}";
        restartCountText.text = $"リスタート回数 {restartCounter.GetCount}";

        backGround.rectTransform.DOLocalMoveY(150f, 1f).SetLoops(1, LoopType.Restart).SetEase(ease);
    }
}
