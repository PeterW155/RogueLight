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
    
    /// <summary>
    /// The number of rounds increased every time the player picks up this object.
    /// </summary>
    public int PickUpIncrease { get; protected set; }

    [Tooltip("The maximum amount of rounds this weapon has."),SerializeField]
    private int _maxRound;
    
    [Tooltip("The damage this weapon deals."),SerializeField]
    private float _damage;

    [Tooltip("The number of rounds increased every time the player picks up this object."), SerializeField]
    private int _pickUpIncrease;

    /// <summary>
    /// This method is called when the firing mouse button is pressed down.
    /// </summary>
    public abstract void FireDown();
    
    /// <summary>
    /// This method is called when the firing mouse button is released.
    /// </summary>
    public abstract void FireUp();

    public virtual void AddRound()
    {
        CurrentRound += PickUpIncrease;
        CurrentRound = Mathf.Min(CurrentRound, MaxRound);
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        MaxRound = _maxRound;
        CurrentRound = MaxRound;
        Damage = _damage;
        PickUpIncrease = _pickUpIncrease;
    }
#endif

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController controller = other.GetComponent<PlayerController>();
            if (!controller)
                return;
            controller.AddWeaponToInventory(this);
            
            // Destroy collider
            Destroy(other);
        }
    }
}
