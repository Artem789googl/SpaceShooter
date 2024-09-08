using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        public static SpaceShip SelectedSpaceShip;

        [SerializeField] private int m_NumLives;
        [SerializeField] private SpaceShip m_PlayerShipPrefab;

        private FollowCamera m_FollowCamera;
        private ShipInputController m_ShipInputController;
        private Transform m_SpawnPoint;

        public FollowCamera FollowCamera => m_FollowCamera;

        public void Construct(FollowCamera followCamera, ShipInputController shipInputController, Transform spawnPoint)
        {
            m_FollowCamera = followCamera;
            m_ShipInputController = shipInputController;
            m_SpawnPoint = spawnPoint;
        }

        private SpaceShip m_Ship;
        public SpaceShip ActiveShip => m_Ship;

        private int m_Score;
        private int m_NumSkills;

        public SpaceShip ShipPrefab {
            get
            {
                if(SelectedSpaceShip == null)
                {
                    return m_PlayerShipPrefab;
                }
                else
                {
                    return SelectedSpaceShip;
                }
            }
        }

        public int Score => m_Score;
        public int NumKills => m_NumSkills;
        public int NumLives => m_NumLives;

        private void Start()
        {
            Respawn();
        }

        private void OnShipDeath()
        {
            m_NumLives--;

            if(m_NumLives > 0)
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            var newPlayerShip = Instantiate(ShipPrefab, m_SpawnPoint.position, m_SpawnPoint.rotation);

            m_Ship = newPlayerShip.GetComponent<SpaceShip>();
            m_Ship.EventOnDeath.AddListener(OnShipDeath);

            m_FollowCamera.SetTarget(m_Ship.transform);
            m_ShipInputController.SetTarget(m_Ship);
        }

        public void AddKill()
        {
            m_NumSkills += 1;
        }

        public void AddScore(int num)
        {
            m_Score += num;
        }
    }
}
