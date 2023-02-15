using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerMove player;

    // �v���C���[�̏������ĕς���
    // �J�n�ʒm
    public IReadOnlyReactiveProperty<bool> StartProp => start;
    private ReactiveProperty<bool> start = new ReactiveProperty<bool>(false);

    // 1�Ń~�X:2�Ń��X�^�[�g
    public IReadOnlyReactiveProperty<int> RestartProp => restart;
    private ReactiveProperty<int> restart = new ReactiveProperty<int>(0);

    // �I���ʒm
    public IReadOnlyReactiveProperty<bool> ClearProp => clear;
    private ReactiveProperty<bool> clear = new ReactiveProperty<bool>(false);

    /// <summary>
    /// �t�F�[�h�A�E�g�̎���
    /// </summary>
    [SerializeField]
    float holdTime = 0.1f;

    /// <summary>
    /// �Q�[��������(�t�F�[�h�A�E�g���I����Ă��瑪��n�߂���)
    /// </summary>
    public float gameTime => timer;
    private float timer = 0;

    /// <summary>
    /// ���l����
    /// </summary>
    public int starCount => star;
    private int star = 0;

    /// <summary>
    /// ���X�^�[�g��(���S�A���X�^�[�g��킸)
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

    // �ꎞ��~������
    //global�ȃC�x���g�������
    //��~�������N���X�̓C�x���g���w�ǂ���
    //�ꎞ��~�̊Ǘ��N���X���C�x���g�𔭍s����
}