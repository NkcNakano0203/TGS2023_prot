using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerMove player;

    // クリアイベント
    public IReadOnlyReactiveProperty<bool> ClearProp => clear;
    private ReactiveProperty<bool> clear = new ReactiveProperty<bool>(false);

    /// <summary>
    /// ゲーム内時間
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

    bool pause = false;

    private void Start()
    {
        // ステージクリアイベント
        player.GetGoal.Where(x => x).Subscribe(x =>
        {
            ClearTime(); 
            clear.Value = true;
        });
        // プレイヤーのミスイベント
        player.DeathProp.Where(x => x).Subscribe(x => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        // ステージ中の星獲得イベント
        player.GetStar.Where(x => x).Subscribe(x => star++);
    }

    void Update()
    {
        if (pause) return;
        timer += Time.deltaTime;
    }

    void ClearTime()
    {
        timer = Mathf.Floor(timer);
    }

    // 一時停止実装案
    //globalなイベントを作って
    //停止したいクラスはイベントを購読して
    //一時停止の管理クラスがイベントを発行する
}