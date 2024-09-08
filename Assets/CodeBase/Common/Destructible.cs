using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Common
{
    /// <summary>
    /// Уничтожаемый объект на сцене. То что может иметь хит поинты.
    /// </summary>

    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// Objest is ignore damage.
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestrucble => m_Indestructible;

        /// <summary>
        /// Start count of hitpoints.
        /// </summary>
        [SerializeField] private int m_HitPoints;
        public int MaxHitpoints => m_HitPoints;


        /// <summary>
        /// Current hitpoins.
        /// </summary>
        private int m_CurrenHitPoints;
        public int HitPoints => m_CurrenHitPoints;

        /// <summary>
        /// View explosion
        /// </summary>
        [SerializeField] private GameObject m_Explosin_View;

        #endregion

        #region Unity Events
        protected virtual void Start()
        {
            m_CurrenHitPoints = m_HitPoints;

            transform.SetParent(null);
        }

        #endregion

        #region Public API
        /// <summary>
        /// Apply damage to object.
        /// </summary>
        /// <param name="damage"> Урон наносимый объекту </param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrenHitPoints -= damage;

            if (m_CurrenHitPoints <= 0)
            {
                OnDeath();
                ExplosinAnim();
            }
        }


        public void ExplosinAnim()
        {
            if (m_Explosin_View == null) return;

            int timeAnim = 0;
            var ex = Instantiate(m_Explosin_View, transform.position, Quaternion.identity);

            if (ex != null)
            {
                timeAnim = ex.GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(0).Length;
            }
            Destroy(ex, timeAnim);
        }

        #endregion
        /// <summary>
        /// Переопредляемое событие уничтожения объекта, когда хит поинты ниже нуля.
        /// </summary>
        protected virtual void OnDeath()
        {
            
            Destroy(gameObject);
            m_EventOnDeath?.Invoke();
        }

        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if(m_AllDestructibles == null)
            {
                m_AllDestructibles = new HashSet<Destructible>();
            }

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

    }
}
