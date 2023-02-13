using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class GameManager : MonoBehaviour
{
    // 開始通知
    public IReadOnlyReactiveProperty<bool> StartProp => start;
    private ReactiveProperty<bool> start = new ReactiveProperty<bool>(false);

    // 終了通知
    public IReadOnlyReactiveProperty<bool> GoalProp => goal;
    private ReactiveProperty<bool> goal = new ReactiveProperty<bool>(false);

    /// <summary>
    /// フェードアウトを待ってスタートしたい
    /// </summary>
    [SerializeField]
    float holdTime = 0.1f;

    void Start()
    {

    }

    void Update()
    {

    }


}
