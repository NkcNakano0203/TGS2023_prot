using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class GameManager : MonoBehaviour
{
    // �J�n�ʒm
    public IReadOnlyReactiveProperty<bool> StartProp => start;
    private ReactiveProperty<bool> start = new ReactiveProperty<bool>(false);

    // �I���ʒm
    public IReadOnlyReactiveProperty<bool> GoalProp => goal;
    private ReactiveProperty<bool> goal = new ReactiveProperty<bool>(false);

    /// <summary>
    /// �t�F�[�h�A�E�g��҂��ăX�^�[�g������
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
