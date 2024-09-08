using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class EntitySpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Destructible[] m_DebrisPrefab;

        [SerializeField] private int m_NumDebris;

        [SerializeField] private CircleArea m_Area;

        [SerializeField] private float m_RandomSpeed;

        private void Start()
        {
            for (int i = 0; i < m_NumDebris; i++)
            {
                SpawnDebris();
            }
        }

        private void SpawnDebris()
        {
            int index = Random.Range(0, m_DebrisPrefab.Length);

            GameObject debris = Instantiate(m_DebrisPrefab[index].gameObject);

            debris.transform.position = m_Area.GetRandomInsideZone();
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

            if(rb != null && m_RandomSpeed > 0)
            {
                rb.velocity = (Vector2) UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
            }
        }

        private void OnDebrisDead()
        {
            SpawnDebris();
        }
    }
}
