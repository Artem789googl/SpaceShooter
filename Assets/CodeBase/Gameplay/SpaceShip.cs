using UnityEngine;
using Common;

namespace SpaceShooter {

    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {

        [SerializeField] private Sprite m_PreviewImage;

        /// <summaru>
        /// Масса для аавтоматической установки у ригида.
        ///</summaru>
        [Header("Spaece ship")]
        [SerializeField] private float m_Mass;

        ///<summary>
        /// Толкающая сила вперёд.
        ///</summary>
        [SerializeField] private float m_Thrust;
        public float Thrust => m_Thrust;


        /// <summary>
        /// Ускорение для корабля.
        /// </summary>

        [SerializeField] private float m_acceleration;

        public void SetAcceleration(float speed)
        {
            m_acceleration = speed;
        }

        ///<summary>
        /// Вращающая сила.
        ///</summary>
        [SerializeField] private float m_Mobility;

        ///<summary>
        /// Максимальная линейная скорость.
        ///</summary>
        [SerializeField] private float m_MaxLinearVelocity;

        ///<summary>
        /// Максимальная вращательная скорость. В градусах/сек.
        ///</summary>
        [SerializeField] private float m_MaxAngulerVelocity;

        ///<summaru>
        /// Сохраненная ссылка на ригид.
        ///</summaru>
        private Rigidbody2D m_Rigid;

        #region Public API

        ///<summary>
        /// Управление линейной тягой. -1.0 до +1.0
        ///</summary>
        public float ThrustControl { get; set; }

        ///<summary>
        /// Управление вращательной тягой. -1.0 до +1.0
        ///</summary>
        public float TorqueControl { get; set; }

        public float MaxLinearVelocity => m_MaxLinearVelocity;
        public float MaxAngularVelocity => m_MaxAngulerVelocity;
        public Sprite PreviewImage => m_PreviewImage;


        #endregion

        #region Unity Event
        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;

            InitOffensive();
        }



        private void FixedUpdate()
        {
            UpdateRigidBody();

            UpdateEnergyRegen();
        }
        #endregion
        /// <summary>
        /// Метод добавления сил кораблю для движения
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigid.AddForce(ThrustControl * (m_Thrust + m_acceleration) *  transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce(-m_Rigid.velocity * ((m_Thrust + m_acceleration) / m_MaxLinearVelocity ) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility *  Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility/m_MaxAngulerVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode) 
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if(m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;

        public void AddEnergy(int e)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy);
        }
        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }

        public bool DrawAmmo(int count)
        {
            if (count == 0) 
                return true;

            if(m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }

            return false;

        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }

            return false;

        }

        public void AssignWeapon(TurretProperties props)
        {
            for(int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }

    }
}
