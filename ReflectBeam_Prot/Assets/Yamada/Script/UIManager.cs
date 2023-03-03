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

    [SerializeField] TextMeshProUGUI starCountText = null;
    [SerializeField] TextMeshProUGUI timeCountText = null;
    [SerializeField] TextMeshProUGUI restartCountText = null;

    [SerializeField] Image image;

    [SerializeField] Ease ease;

    void Start()
    {
        gm.ClearProp.Where(x => x).Subscribe(x => OnClear());
    }

    void Update()
    {

    }

    void OnClear()
    {
        starCountText.text = $"Star:{gm.starCount}";
        timeCountText.text = $"�N���A����:{gm.gameTime}";
        restartCountText.text = $"���X�^�[�g��:{gm.restartCount}";

        image.rectTransform.DOLocalMoveY(0f, 1f).SetLoops(1, LoopType.Restart).SetEase(ease);
    }
}
