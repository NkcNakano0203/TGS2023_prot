using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class GameManager : MonoBehaviour
{
    // �v���C���[�̏������ĕς���
    // �J�n�ʒm
    public IReadOnlyReactiveProperty<bool> StartProp => start;
    private ReactiveProperty<bool> start = new ReactiveProperty<bool>(false);
    // �I���ʒm
    public IReadOnlyReactiveProperty<bool> GoalProp => goal;
    private ReactiveProperty<bool> goal = new ReactiveProperty<bool>(false);

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

    void Start()
    {

    }

    void Update()
    {

    }
}