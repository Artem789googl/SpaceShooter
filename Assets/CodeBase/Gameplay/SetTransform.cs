using UnityEngine;

namespace SpaceShooter
{
    public class SetTransform : MonoBehaviour
    {
        [SerializeField] private Player m_Player;

        public void TransformVoid(Transform tr)
        {
            m_Player.ActiveShip.transform.position = tr.position;
        }
    }
}
