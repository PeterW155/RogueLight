using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public List<Weapon> Weapons { get; private set; }

    [SerializeField] private Transform _weaponMount;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject _defaultWeapon;
    
    private Rigidbody2D _rigidbody2D;
    private Weapon _currentWeapon;
    private int _currentWeaponIndex = 0;
    private int _currentWeaponMax = 0;
    private int _maxWeaponCount = 5;
    private Vector2 _direction;

    public void AddWeaponToInventory(Weapon weapon)
    {
        // If already have the type of weapon in inventory, increase its load and switch to it
        Weapon weaponInInventory = Weapons.FirstOrDefault(x => x.GetType() == weapon.GetType());
        if (weaponInInventory)
        {
            weaponInInventory.AddRound();
            int weaponIndex = Weapons.IndexOf(weaponInInventory);
            SwitchToWeapon(weaponIndex);
        } 
        // If don't have weapon in inventory and inventory is not full yet, add to the end and switch to it
        else if (_currentWeaponMax < _maxWeaponCount)
        {
            GameObject go = Instantiate(weapon.PrefabReference, _weaponMount);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = quaternion.identity;
            
            // Collider for pickup detection is not needed once equipped.
            go.GetComponent<Weapon>().PickupBox.enabled = false;

            _currentWeaponMax++;
            Weapons.Add(go.GetComponent<Weapon>());
            SwitchToWeapon(_currentWeaponMax - 1);
        }
        else
        {
            Debug.LogWarning("Inventory is full");
        }
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Weapons = new List<Weapon>();
        AddWeaponToInventory(_defaultWeapon.GetComponent<Weapon>());
    }

    private void Update()
    {
        GetSwapWeaponInput();
        GetDirectionInput();
        Rotate();
        GetFireInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        _camera.position = transform.position - 10.0f * Vector3.forward;
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

    private void GetDirectionInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _direction = new Vector2(horizontal, vertical).normalized;
    }

    private void GetFireInput()
    {
        if (Input.GetButtonDown("Fire1"))
            Fire(true);
        else if (Input.GetButtonUp("Fire1"))
            Fire(false);
    }

    private void Rotate()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition -= new Vector2(0.5f * Screen.width, 0.5f * Screen.height);
        mousePosition.Normalize();
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg - 90.0f;
        _rigidbody2D.MoveRotation(angle);
    }
    
    private void SwitchToWeapon(int index)
    {
        if (index < 0 || index > _currentWeaponMax || !Weapons[index])
            return;
        
        Weapons[_currentWeaponIndex].gameObject.SetActive(false);
        _currentWeaponIndex = index;
        _currentWeapon = Weapons[index];
        Weapons[index].gameObject.SetActive(true);
    }

    private void Move()
    {
        Vector2 currentPosition = _rigidbody2D.position;
        _rigidbody2D.MovePosition(currentPosition + _moveSpeed * Time.fixedDeltaTime * _direction);
    }

    private void Fire(bool isFireDown)
    {
        if (!_currentWeapon)
            return;
        
        if (isFireDown)
           _currentWeapon.FireDown();
        else
            _currentWeapon.FireUp();
    }
}