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
    // �I���ʒm
    // 1�ŃQ�[���I�[�o�[:2�ŃQ�[���N���A
    public IReadOnlyReactiveProperty<int> EndProp => end;
    private ReactiveProperty<int> end = new ReactiveProperty<int>(0);

    //TODO:UI�Ɍ����Ď��ԁA���A���X�^�[�g�������J����

    /// <summary>
    /// �t�F�[�h�A�E�g��҂��ăX�^�[�g������
    /// </summary>
    [SerializeField]
    float holdTime = 0.1f;

    /// <summary>
    /// �t�F�[�h�A�E�g���I����Ă���̎���
    /// </summary>
    public float gameTime => timer;
    private float timer = 0;

    /// <summary>
    /// ���̐�
    /// </summary>
    public int starCount => star;
    private int star = 0;

    /// <summary>
    /// ���X�^�[�g��
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

    // �ꎞ��~������
    //global�ȃC�x���g�������
    //��~�������N���X�̓C�x���g���w�ǂ���
    //�ꎞ��~�̊Ǘ��N���X���C�x���g�𔭍s����
}