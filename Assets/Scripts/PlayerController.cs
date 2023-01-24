using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public Weapon[] Weapons { get; private set; }
    public int MaxWeaponCount => 5;

    [SerializeField] private Transform _weaponMount;
    private Rigidbody2D _rigidbody2D;
    private Weapon _currentWeapon;
    private int _currentWeaponIndex;
    private int _currentWeaponMax;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Weapons = new Weapon[5];
        Weapons[0] = _weaponMount.gameObject.AddComponent<HandCrankFlashlight>(); //TODO: wrong
        _currentWeaponIndex = 0;
        _currentWeaponMax = 1;
    }

    private void Update()
    {
        GetSwapWeaponInput();
    }

    private void GetSwapWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchToWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchToWeapon(4);
        }
    }

    private void SwitchToWeapon(int index)
    {
        if (index > _currentWeaponMax || index == _currentWeaponIndex)
            return;
        _currentWeaponIndex = index;
        _currentWeapon = Weapons[index];
    }
}