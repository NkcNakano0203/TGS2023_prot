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
    // 終了通知
    // 1でゲームオーバー:2でゲームクリア
    public IReadOnlyReactiveProperty<int> EndProp => end;
    private ReactiveProperty<int> end = new ReactiveProperty<int>(0);

    //TODO:UIに向けて時間、星、リスタート数を公開する

    /// <summary>
    /// フェードアウトを待ってスタートしたい
    /// </summary>
    [SerializeField]
    float holdTime = 0.1f;

    /// <summary>
    /// フェードアウトが終わってからの時間
    /// </summary>
    public float gameTime => timer;
    private float timer = 0;

    /// <summary>
    /// 星の数
    /// </summary>
    public int starCount => star;
    private int star = 0;

    /// <summary>
    /// リスタート数
    /// </summary>
    public int restartCount => restart;
    private int restart = 0;

    private void Start()
    {
        player.DeathProp.Where(x => x).Subscribe(x => end.Value = 1);
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