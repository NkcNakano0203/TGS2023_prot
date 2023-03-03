using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerMove player;

    // �N���A�C�x���g
    public IReadOnlyReactiveProperty<bool> ClearProp => clear;
    private ReactiveProperty<bool> clear = new ReactiveProperty<bool>(false);

    /// <summary>
    /// �Q�[��������
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

    bool pause = false;

    private void Start()
    {
        // �X�e�[�W�N���A�C�x���g
        player.GetGoal.Where(x => x).Subscribe(x =>
        {
            ClearTime(); 
            clear.Value = true;
        });
        // �v���C���[�̃~�X�C�x���g
        player.DeathProp.Where(x => x).Subscribe(x => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        // �X�e�[�W���̐��l���C�x���g
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

    // �ꎞ��~������
    //global�ȃC�x���g�������
    //��~�������N���X�̓C�x���g���w�ǂ���
    //�ꎞ��~�̊Ǘ��N���X���C�x���g�𔭍s����
}