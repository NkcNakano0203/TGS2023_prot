using UniRx;

// �g�p����using�������K�v������
namespace Pause
{
    public static class PauseManager
    {
        /// <summary>
        /// �w�ǂ��鑤��Subscribe���邱��
        /// �Ǘ��҈ȊO�C�x���g�����s���Ă͂Ȃ�Ȃ�
        /// </summary>
        public static ReactiveProperty<bool> pause = new ReactiveProperty<bool>(false);
    }
}