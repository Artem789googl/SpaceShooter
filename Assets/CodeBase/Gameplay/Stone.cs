using UnityEngine;
using Common;

namespace SpaceShooter {
    public class Stone : Destructible
    {

        public enum Size
        {
            Small,
            Normal,
            Big,
            Huge
        }

        [SerializeField] private Size size;
        [SerializeField] private float spawnUpForce;

        private void Awake()
        {
            EventOnDeath.AddListener(OnStoneDestroyed);
            SetSize(size);
        }


        private void OnStoneDestroyed()
        {
            if (size != Size.Small)
            {
                SpawnStones();
            }
        }

        private void SpawnStones()
        {

            for (int i = 0; i < 2; i++)
            {
                Stone stone = Instantiate(this, transform.position + new Vector3(i, i, 0), Quaternion.identity);
                stone.SetSize(size - 1);
            }
        }

        public void SetSize(Size size)
        {
            if (size < 0) return;
            transform.localScale = GetVectorFromSize(size);
            this.size = size;
        }

        private Vector3 GetVectorFromSize(Size size)
        {
            if (size == Size.Huge) return new Vector3(1, 1, 1);
            if (size == Size.Big) return new Vector3(0.75f, 0.75f, 0.75f);
            if (size == Size.Normal) return new Vector3(0.6f, 0.6f, 0.6f);
            if (size == Size.Small) return new Vector3(0.4f, 0.4f, 0.4f);

            return Vector3.one;
        }
    }
}
