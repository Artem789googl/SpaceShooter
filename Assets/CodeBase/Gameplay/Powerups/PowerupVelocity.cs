using UnityEngine;

namespace SpaceShooter
{
    public class PowerupVelocity : Powerup
    {

        [SerializeField] private float m_LifeTime;
        protected override void OnPickedUp(SpaceShip ship)
        {

            Player player = FindObjectOfType<Player>();

            if (player == null) return;
            player.GetComponent<ShipInputController>().UseShift(m_LifeTime);
        }
    }
}
