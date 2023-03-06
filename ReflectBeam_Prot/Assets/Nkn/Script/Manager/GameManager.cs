using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerMove player;

    /// <summary>
    /// リスタート数(死亡、リスタート問わず)
    /// </summary>
    [SerializeField]
    RestartCounter restartCounter;

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
    public bool GetItem => item;
    private bool item = false;

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
        player.DeathProp.Where(x => x).Subscribe(x =>
        {
            restartCounter.Add();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        // ステージ中の星獲得イベント
        player.GetStar.Where(x => x).Subscribe(x => item = true);
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
}