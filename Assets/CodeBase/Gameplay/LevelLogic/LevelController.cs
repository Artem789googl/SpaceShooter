using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelController : SingletonBase<LevelController>
    {
        private const string MAINMENUSSCENENAME = "main_menu";

        public event UnityAction LevelPassed;
        public event UnityAction LevelLost;

        [SerializeField] private LevelCondition[] m_Conditions;

        private bool m_IsLevelCompleted;

        private float m_LevelTime;

        private LevelSequencesController m_LevelSequencesController;
        private LevelProperties m_CurrentLevelPropetries;

        public float LevelTime => m_LevelTime;

        private void Start()
        {
            Time.timeScale = 1;
            m_LevelTime = 0;

            m_LevelSequencesController = LevelSequencesController.Instance;
            m_CurrentLevelPropetries = m_LevelSequencesController.GetCurrentLoadedLevel();
        }
        private void Update()
        {

            if (m_IsLevelCompleted == false)
            {
                m_LevelTime += Time.deltaTime;
                CheckLevelConditions();
            }


            if(Player.Instance.NumLives == 0)
            {
                Lose();
            }
        }

        private void CheckLevelConditions()
        {


            int numCompleted = 0;

            for (int i = 0; i < m_Conditions.Length; i++)
            {
                if (m_Conditions[i].IsCompleted)
                {
                    numCompleted++;
                }
            }

            if (numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;

                Pass();
            }
        }

        private void Lose()
        {
            LevelLost?.Invoke();
            Time.timeScale = 0;
        }

        private void Pass()
        {
            LevelPassed?.Invoke();
            Time.timeScale = 0;
        }

        public void LoadNextLevel()
        {


            if(m_LevelSequencesController.CurrentLevelIsLast() == false)
            {
                string nextLevelSceneName = m_LevelSequencesController.GetNextLevelPropetries(m_CurrentLevelPropetries).SceneName;
                SceneManager.LoadScene(nextLevelSceneName);
            }
            else
            {
                SceneManager.LoadScene(MAINMENUSSCENENAME);
            }
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(m_CurrentLevelPropetries.SceneName);
        }
    }
}
