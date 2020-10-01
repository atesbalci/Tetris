using DG.Tweening;

namespace Tetris.Helpers.Tweens
{
    public static class TweenUtils
    {
        public static void KillIfActive(this Tween tween, bool complete = false)
        {
            if (tween != null && tween.active)
            {
                tween.Kill(complete);
            }
        }
    }
}