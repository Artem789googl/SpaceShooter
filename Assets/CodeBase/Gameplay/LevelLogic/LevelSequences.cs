using UnityEngine;


namespace SpaceShooter
{
    [CreateAssetMenu(fileName = "LevelSequences", menuName = "Logic/LevelSequencesController")]
    public class LevelSequences : ScriptableObject
    {
        public LevelProperties[] LevelsPropetries;
    }
}
