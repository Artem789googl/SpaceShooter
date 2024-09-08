using UnityEngine;

namespace SpaceShooter
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_MainPanel;
        [SerializeField] private GameObject m_ShipSelelctionPanel;
        [SerializeField] private GameObject m_LevelSelectionPanel;

        private void Start()
        {
            EX_ShowMainPanel();
        }

        public void EX_ShowMainPanel()
        {
            m_MainPanel.SetActive(true);
            m_LevelSelectionPanel.SetActive(false);
            m_ShipSelelctionPanel.SetActive(false);
        }

        public void EX_ShowShipSelectionPanel()
        {
            m_ShipSelelctionPanel.SetActive(true);
            m_MainPanel.SetActive(false);
        }

        public void EX_ShowLevelSelectionPanel()
        {
            m_LevelSelectionPanel.SetActive(true);
            m_MainPanel.SetActive(false);
        }

        public void EX_Quit()
        {
            Application.Quit();
        }
    }
}
