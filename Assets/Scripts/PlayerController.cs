using System;
using UnityEngine;
using System.Linq;
using Unity.Mathematics;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public Weapon[] Weapons { get; private set; }

    [SerializeField] private Transform _weaponMount;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rigidbody2D;
    private Weapon _currentWeapon;
    private int _currentWeaponIndex;
    private int _maxWeaponCount = 5;
    private int _currentWeaponMax;
    private Vector2 _direction;

    public void AddWeaponToInventory(Weapon weapon)
    {
        // If already have the type of weapon in inventory, increase its load and switch to it
        Weapon weaponInInventory = Weapons.FirstOrDefault(x => x.GetType() == weapon.GetType());
        if (weaponInInventory != null)
        {
            weaponInInventory.AddRound();
            int weaponIndex = Array.IndexOf(Weapons, weaponInInventory);
            SwitchToWeapon(weaponIndex);
        } 
        // If don't have weapon in inventory and inventory is not full yet, add to the end and switch to it
        else if (_currentWeaponMax < _maxWeaponCount)
        {
            _currentWeaponMax++;
            GameObject go = new GameObject($"Temp {weapon}", weapon.GetType()); //TODO: might just get the parent class type
            go.transform.SetParent(_weaponMount);
            go.transform.localPosition = Vector2.zero;
            go.transform.localRotation = quaternion.identity;
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
        Weapons = new Weapon[5];
        Weapons[0] = _weaponMount.gameObject.AddComponent<HandCrankFlashlight>(); //TODO: wrong
        _currentWeaponIndex = 0;
        _currentWeaponMax = 1;
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
        if (index < 0 || index > _currentWeaponMax || index == _currentWeaponIndex || !Weapons[index])
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