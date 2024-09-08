using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";

        [SerializeField] private float m_VelocityDamageModifier;

        [SerializeField] private float m_DamageConstant;

        public void SetDamageComstant(float damage)
        {
            m_DamageConstant = damage;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;


            var destructble = transform.root.GetComponent<Destructible>();

            if(destructble != null)
            {
                destructble.ApplyDamage((int) m_DamageConstant + (int) (m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
            }
        }
    }
}
