using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class GameManager : MonoBehaviour
{
    // プレイヤーの情報を見て変える
    // 開始通知
    public IReadOnlyReactiveProperty<bool> StartProp => start;
    private ReactiveProperty<bool> start = new ReactiveProperty<bool>(false);
    // 終了通知
    public IReadOnlyReactiveProperty<bool> GoalProp => goal;
    private ReactiveProperty<bool> goal = new ReactiveProperty<bool>(false);

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

    void Start()
    {

    }

    void Update()
    {

    }
}