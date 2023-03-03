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

    [SerializeField] Image item = null;
    [SerializeField] TextMeshProUGUI timeCountText = null;
    [SerializeField] TextMeshProUGUI restartCountText = null;

    [SerializeField] Image image;

    [SerializeField] Ease ease;

    private Sprite sprite;

    void Start()
    {
        gm.ClearProp.Where(x => x).Subscribe(x => OnClear());
    }

    void Update()
    {

    }

    void OnClear()
    {
        sprite = Resources.Load<Sprite>("GearReal");
        item = this.GetComponent<Image>();
        item.sprite = sprite;


        timeCountText.text = $"クリアタイム {gm.gameTime}";
        restartCountText.text = $"リスタート回数 {gm.restartCount}";

        image.rectTransform.DOLocalMoveY(0f, 1f).SetLoops(1, LoopType.Restart).SetEase(ease);
    }
}
