using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] RestartCounter restartCounter;

    [SerializeField] Image item = null;
    [SerializeField] Image backGround;
    [SerializeField] TextMeshProUGUI timeCountText = null;
    [SerializeField] TextMeshProUGUI restartCountText = null;

    [SerializeField] Ease ease;
    [SerializeField] Sprite ItemSprite;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Button firstButton;
    [SerializeField] Button nextButton;

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

        if (firstButton.gameObject != null)
        {
            firstButton.interactable = true;
        }
        if (nextButton.gameObject != null)
        {
            nextButton.interactable = true;
        }

        eventSystem.SetSelectedGameObject(firstButton.gameObject);

        timeCountText.text = $"クリアタイム {gm.gameTime}秒";
        restartCountText.text = $"リスタート回数 {restartCounter.GetCount}回";

        backGround.rectTransform.DOLocalMoveY(120f, 1f).SetLoops(1, LoopType.Restart).SetEase(ease);
    }
}
