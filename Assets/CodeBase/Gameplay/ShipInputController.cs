using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class ShipInputController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

       
        public void SetTarget(SpaceShip ship) => m_TargetShip = ship;

        [SerializeField] private ControlMode m_ControlMode;

        public void Construct(VirtualGamePad virtualGamePad)
        {
            m_VirtualGamePad = virtualGamePad;
        }

        private SpaceShip m_TargetShip;
        private VirtualGamePad m_VirtualGamePad;

        private void Start()
        {
            if(m_ControlMode == ControlMode.Keyboard)
            {
                m_VirtualGamePad.VirtualJostick.gameObject.SetActive(false);

                m_VirtualGamePad.MobileFirePrimary.gameObject.SetActive(false);
                m_VirtualGamePad.MobileFireSecondary.gameObject.SetActive(false);
            }
            else
            {
                m_VirtualGamePad.VirtualJostick.gameObject.SetActive(true);

                m_VirtualGamePad.MobileFirePrimary.gameObject.SetActive(true);
                m_VirtualGamePad.MobileFireSecondary.gameObject.SetActive(true);
            }
               
            /*
            // Автоматическое определение джостика
            if (Application.isMobilePlatform)
            {
                m_ControlMode = ControlMode.Mobile;
                m_MobileJostick.gameObject.SetActive(true);
            }
            else
            {
                m_ControlMode = ControlMode.Keyboard;
                m_MobileJostick.gameObject.SetActive(false);
            }
             */
        }
        private void Update()
        {
            if (m_TargetShip == null) return;
            
            if(m_ControlMode == ControlMode.Keyboard)
            {
                ControlKeyboard();
            }

            if (m_ControlMode == ControlMode.Mobile)
            {
                ControlMobile();
            }
           
        }

        private void ControlMobile()
        {
            var dir = m_VirtualGamePad.VirtualJostick.Value;
            m_TargetShip.ThrustControl = dir.y;
            m_TargetShip.TorqueControl = -dir.x;

            if (m_VirtualGamePad.MobileFirePrimary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (m_VirtualGamePad.MobileFireSecondary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }
        }

        private void ControlKeyboard()
        {
            float thrust = 0f;
            float torque = 0f;

            m_TargetShip.SetAcceleration(0f);

            if (Input.GetKey(KeyCode.UpArrow)) thrust = 1.0f;
            if (Input.GetKey(KeyCode.DownArrow)) thrust = -1.0f;
            if (Input.GetKey(KeyCode.LeftArrow)) torque = 1.0f;
            if (Input.GetKey(KeyCode.RightArrow)) torque = -1.0f;

            m_Timer += Time.deltaTime;

            if(m_Timer < m_LifeTime)
            {
                if ((Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.RightArrow)) && Input.GetKey(KeyCode.RightShift))
                { m_TargetShip.SetAcceleration(10000f); }
                
            }
            else
            {
                m_LifeTime = 0;
            }

            


            if (Input.GetKey(KeyCode.Space))
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (Input.GetKey(KeyCode.X))
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }

            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }

        private float m_LifeTime;
        private float m_Timer;
        public float UseShift(float time)
        {
            m_Timer = 0;
            return m_LifeTime = time;
        }
    }
}
