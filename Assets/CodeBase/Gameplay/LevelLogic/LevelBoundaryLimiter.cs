using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Ограничеитель позиции. Работает в связке со скриптом LevelBoundary если таковой имеется на сцене
    /// Кидается на объект который надо ограничить
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundery.Instance == null) return;

            var lb = LevelBoundery.Instance;
            var r = LevelBoundery.Instance.Radius;

            if(transform.position.magnitude > r)
            {
                if(lb.LimitMode == LevelBoundery.Mode.Limit)
                {
                    transform.position = transform.position.normalized * r;
                }

                if (lb.LimitMode == LevelBoundery.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * r;
                }
            }

        }
    }
}
