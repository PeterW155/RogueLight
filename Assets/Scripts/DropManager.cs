using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [System.Serializable]
    struct WeaponDrop
    {
        public Weapon Weapon;
        [Range(0.0f, 1.0f)]
        public float DropRate;
    }
    public static DropManager Instance { get; private set; }

    [SerializeField] private WeaponDrop[] _drops;
    private IEnumerable<WeaponDrop> _orderedDrops;

    public void NotifyDeath(SimpleEnemy enemy)
    {
        var randomValue = Random.Range(0.0f, 1.0f);
        foreach (var weaponDrop in _orderedDrops)
        {
            if (randomValue > weaponDrop.DropRate)
                continue;
            Instantiate(weaponDrop.Weapon, enemy.transform.position, Quaternion.identity);
        }
    }

    private void Awake()
    {
        Instance = this;
        _orderedDrops = _drops.OrderBy(x => x.DropRate);
    }
}