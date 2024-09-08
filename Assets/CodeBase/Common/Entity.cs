using UnityEngine;

/// <summary>
/// Main class of all interactive play object on the scene.
/// </summary>
public abstract class Entity : MonoBehaviour
{
    /// <summary>
    /// Name of object for user.
    /// </summary>
    [SerializeField]
    private string m_Nickname;
    public string Nickname => m_Nickname;
}
