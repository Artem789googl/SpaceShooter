using UnityEngine;

namespace SpaceShooter
{
    public class PowerupShield : Powerup
    {
        [SerializeField] private GameObject m_ShieldPrefab;

        protected override void OnPickedUp(SpaceShip ship)
        {
            GameObject e = Instantiate(m_ShieldPrefab, ship.gameObject.transform);
            e.transform.SetParent(ship.transform);
        }
    }
}
