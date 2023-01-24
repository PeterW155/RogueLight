using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    /// <summary>
    /// The maximum amount of rounds this weapon has.
    /// </summary>
    public int MaxRound { get; protected set; } = 0;
    
    /// <summary>
    /// The current amount of rounds this weapon has.
    /// </summary>
    public int CurrentRound { get; protected set; } = 0;

    /// <summary>
    /// The damage this weapon deals.
    /// </summary>
    public float Damage { get; protected set; } = 0.0f;

    [Tooltip("The maximum amount of rounds this weapon has."),SerializeField] private int _maxRound;
    
    [Tooltip("The damage this weapon deals."),SerializeField] private float _damage;

    /// <summary>
    /// This method is called when the firing mouse button is pressed down.
    /// </summary>
    public abstract void FireDown();
    
    /// <summary>
    /// This method is called when the firing mouse button is released.
    /// </summary>
    public abstract void FireUp();

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        MaxRound = _maxRound;
        Damage = _damage;
    }
#endif
}
