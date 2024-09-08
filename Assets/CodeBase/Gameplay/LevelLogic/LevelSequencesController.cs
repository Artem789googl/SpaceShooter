using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequencesController : SingletonBase<LevelSequencesController>
    {
        public LevelSequences LevelSequences;

        public bool CurrentLevelIsLast()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            string lastSceneName = LevelSequences.LevelsPropetries[LevelSequences.LevelsPropetries.Length - 1].SceneName;

            return lastSceneName == sceneName;
        }

        public LevelProperties GetCurrentLoadedLevel()
        {
            string sceneName = SceneManager.GetActiveScene().name;

            for (int i = 0; i < LevelSequences.LevelsPropetries.Length; i++)
            {
                if (LevelSequences.LevelsPropetries[i].SceneName == sceneName) return LevelSequences.LevelsPropetries[i];
            }

            Debug.LogError("Level propetries not found!");
            return null;
        }

        public LevelProperties GetNextLevelPropetries(LevelProperties levelProperties)
        {
            for (int i = 0; i < LevelSequences.LevelsPropetries.Length; i++)
            {
                if(LevelSequences.LevelsPropetries[i].SceneName == levelProperties.SceneName)
                {
                    if(i < LevelSequences.LevelsPropetries.Length-1)
                        return LevelSequences.LevelsPropetries[i + 1];
                }
            }


            Debug.LogError("Level propetries is last!");
            return null;
        }
    }
}
