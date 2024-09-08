using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public abstract class ProjectileBase : Entity
    {

        [SerializeField] private float m_Velocity;
        [SerializeField] private float m_Lifetime;
        [SerializeField] private int m_Damege;


        protected virtual void OnHit(Destructible destructible) { }
        protected virtual void OnHit(Collider2D collider2D) { }
        protected virtual void OnProjectileLifeEnd(Collider2D coll, Vector2 pos) { }

        private float m_Timer;
        protected Destructible m_Parent;

        private void Update()
        {
            float steplength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * steplength;

            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, steplength);

            if (hit)
            {

                OnHit(hit.collider);
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                if(dest != null && dest != m_Parent)
                {
                    dest.ApplyDamage(m_Damege);

                    OnHit(dest);

                    
                }

                OnProjectileLifeEnd(hit.collider, hit.point);

            }

            m_Timer += Time.deltaTime;

            if(m_Timer > m_Lifetime)
            {
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            transform.position += new Vector3(step.x, step.y, 0);

        }

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
    }
}
