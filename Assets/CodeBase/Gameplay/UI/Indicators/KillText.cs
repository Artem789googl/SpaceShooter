using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class KillText : MonoBehaviour
    {
        [SerializeField] private Text m_Text;

        private float lastKillText;

        private void Update()
        {
            int numKill = Player.Instance.NumKills;

            if (lastKillText != numKill)
            {
                m_Text.text = "Kill: " + numKill.ToString();
                lastKillText = numKill;
            }

        }
    }
}
