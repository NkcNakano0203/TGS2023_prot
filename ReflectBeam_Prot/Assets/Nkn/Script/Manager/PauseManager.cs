using UniRx;

// 使用時はusingを書く必要がある
public static class PauseManager
{
    public static MessageBroker pause = new MessageBroker();
    static void HOGE()
    {
        // ポーズを受け取る側
        pause.Receive<Pause>().Subscribe();
        MessageBroker.Default.Receive<Pause>().Subscribe();
        // イベントの発行する側
        pause.Publish(new Pause(true));
    }
    /// <summary>
    /// 購読する側はSubscribeすること
    /// 管理者以外イベントを実行してはならない
    /// </summary>
    //public static ReactiveProperty<bool> pause = new ReactiveProperty<bool>(false);
}

// 受け取る側の例
public class PIYO
{
    bool pauseflg;
    void Start()
    {
        PauseManager.pause.Receive<Pause>().Subscribe(x => pauseflg = x.flg);
    }
    void Update()
    {
        if (pauseflg) return;
    }
}

public struct Pause
{
    public bool flg;

    public Pause(bool flg)
    {
        this.flg = flg;
    }
}