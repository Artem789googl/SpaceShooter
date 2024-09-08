using UnityEngine;

namespace SpaceShooter
{
    public class TeleportToTarget : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;
        [SerializeField] private Player m_Player;
        public void Start()
        {
            m_Player.ActiveShip.EventOnDeath.AddListener(Teleport);
        }

        public void Teleport()
        {
            m_Player.ActiveShip.transform.position = m_Target.transform.position;
            m_Player.ActiveShip.EventOnDeath.AddListener(Teleport);
        }
    }
}
