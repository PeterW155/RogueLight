using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
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
    
    /// <summary>
    /// The Prefab associated with this weapon type.
    /// </summary>
    public GameObject PrefabReference { get; protected set; }
    
    /// <summary>
    /// The Collider2D that is used to detect player picking it up.
    /// </summary>
    public CircleCollider2D PickupBox { get; protected set; }
    
    /// <summary>
    /// The Collider2D that is enabled when firing.
    /// </summary>
    public Collider2D DamageBox { get; protected set; }

    [Tooltip("The maximum amount of rounds this weapon has."),SerializeField]
    private int _maxRound;
    
    [Tooltip("The damage this weapon deals."),SerializeField]
    private float _damage;

    [Tooltip("The number of rounds increased every time the player picks up this object."), SerializeField]
    private int _pickUpIncrease;

    [Tooltip("The Prefab associated with this weapon type."), SerializeField]
    private GameObject _prefabReference;
    
    [Tooltip("The Collider2D that is used to detect player picking it up."), SerializeField]
    private CircleCollider2D _pickupBox;
    
    [Tooltip("The Collider2D that is enabled when firing."), SerializeField]
    private Collider2D _damageBox;

    /// <summary>
    /// FireDown is called when the firing mouse button is pressed down.
    /// </summary>
    public abstract void FireDown();
    
    /// <summary>
    /// FireUp is called when the firing mouse button is released.
    /// </summary>
    public abstract void FireUp();

    public virtual void AddRound()
    {
        CurrentRound += PickUpIncrease;
        CurrentRound = Mathf.Min(CurrentRound, MaxRound);
    }

    protected virtual void Awake()
    {
        MaxRound = _maxRound;
        CurrentRound = MaxRound;
        Damage = _damage;
        PickUpIncrease = _pickUpIncrease;
        PrefabReference = _prefabReference;
        PickupBox = _pickupBox;
        DamageBox = _damageBox;

        PickupBox.enabled = true; // Will be set to false by PlayerController when being picked up
        PickupBox.isTrigger = true;
        DamageBox.enabled = false; // Only turn on when firing
        DamageBox.isTrigger = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerController controller = col.GetComponent<PlayerController>();
            if (!controller)
                return;
            controller.AddWeaponToInventory(this);
            
            Destroy(gameObject);
        }

        // If PickupBox is disabled, that means the weapon is used as a weapon rather than a pickup
        // Then, when colliding with an Enemy, it should deal damage
        if (col.CompareTag("Enemy") && !PickupBox.enabled)
        {
            //TODO: Do damage to enemy
        }
    }
}
