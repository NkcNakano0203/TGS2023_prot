using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerMove player;

    // プレイヤーの情報を見て変える
    // 開始通知
    public IReadOnlyReactiveProperty<bool> StartProp => start;
    private ReactiveProperty<bool> start = new ReactiveProperty<bool>(false);

    // 1でミス:2でリスタート
    public IReadOnlyReactiveProperty<int> RestartProp => restart;
    private ReactiveProperty<int> restart = new ReactiveProperty<int>(0);

    // 終了通知
    public IReadOnlyReactiveProperty<bool> ClearProp => clear;
    private ReactiveProperty<bool> clear = new ReactiveProperty<bool>(false);

    /// <summary>
    /// フェードアウトの時間
    /// </summary>
    [SerializeField]
    float holdTime = 0.1f;

    /// <summary>
    /// ゲーム内時間(フェードアウトが終わってから測り始めたい)
    /// </summary>
    public float gameTime => timer;
    private float timer = 0;

    /// <summary>
    /// 星獲得数
    /// </summary>
    public int starCount => star;
    private int star = 0;

    /// <summary>
    /// リスタート数(死亡、リスタート問わず)
    /// </summary>
    public int restartCount => restartCnt;
    private int restartCnt = 0;

    private void Start()
    {
        player.DeathProp.Where(x => x).Subscribe(x => clear.Value = 1);
        player.GetStar.Where(x => x).Subscribe(x => star++);
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    // 一時停止実装案
    //globalなイベントを作って
    //停止したいクラスはイベントを購読して
    //一時停止の管理クラスがイベントを発行する
}