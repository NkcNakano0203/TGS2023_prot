using UniRx;

// �g�p����using�������K�v������
public static class PauseManager
{
    public static MessageBroker pause = new MessageBroker();
    static void HOGE()
    {
        // �|�[�Y���󂯎�鑤
        pause.Receive<Pause>().Subscribe();
        MessageBroker.Default.Receive<Pause>().Subscribe();
        // �C�x���g�̔��s���鑤
        pause.Publish(new Pause(true));
    }
    /// <summary>
    /// �w�ǂ��鑤��Subscribe���邱��
    /// �Ǘ��҈ȊO�C�x���g�����s���Ă͂Ȃ�Ȃ�
    /// </summary>
    //public static ReactiveProperty<bool> pause = new ReactiveProperty<bool>(false);
}

// �󂯎�鑤�̗�
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