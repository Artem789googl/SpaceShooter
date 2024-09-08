using UnityEngine;


namespace SpaceShooter
{
    public class Shield : MonoBehaviour
    {

        [SerializeField] private float m_Lifetime;

        private float m_Timer;

        private void Update()
        {
            CollisionDamageApplicator dam = transform.root.GetComponent<CollisionDamageApplicator>();

            if (dam == null) return;

            dam.SetDamageComstant(0);


            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
            {
                dam.SetDamageComstant(100);
                Destroy(gameObject);
            }
        }
    }
}
