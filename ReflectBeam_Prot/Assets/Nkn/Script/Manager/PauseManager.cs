using UniRx;

// 使用時はusingを書く必要がある
namespace Pause
{
    public static class PauseManager
    {
        /// <summary>
        /// 購読する側はSubscribeすること
        /// 管理者以外イベントを実行してはならない
        /// </summary>
        public static ReactiveProperty<bool> pause = new ReactiveProperty<bool>(false);
    }
}