using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSelectionButton : MonoBehaviour
    {
       
        [SerializeField] private Text m_LevelTitle;
        [SerializeField] private Image m_PreviewImage;

        private LevelProperties m_LevelPropetries;
        public void SetLevelPropetries(LevelProperties levelProperties)
        {
            m_LevelPropetries = levelProperties;

            m_PreviewImage.sprite = m_LevelPropetries.PreviewImage;
            m_LevelTitle.text = m_LevelPropetries.Title; 
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(m_LevelPropetries.SceneName);
        }
    }
}
