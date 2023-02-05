using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    [System.Serializable]
    struct IconCountPair
    {
        public Image Icon;
        public Text Count;
    }

    [SerializeField] private IconCountPair[] _countPairs;

    private PlayerController _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _player.WeaponFired += OnWeaponFired;
        _player.WeaponAdded += OnWeaponAdded;
        _player.WeaponSwitched += OnWeaponSwitched;
        GameManager.Instance.GameLost += OnGameLost;

        foreach (var pair in _countPairs)
        {
            pair.Icon.enabled = false;
            pair.Count.enabled = false;
        }

        // OnWeaponAdded and OnWeaponSwitched won't be invoked on start because the events happen on Awake in PlayerController
        // So we need to manually load the icon and readjust the opacity
        LoadIcon(0, _player.Weapons[0]);
        _countPairs[0].Icon.color = Color.white;
    }
    private void OnDestroy()
    {
        _player.WeaponFired -= OnWeaponFired;
        _player.WeaponAdded -= OnWeaponAdded;
        _player.WeaponSwitched -= OnWeaponSwitched;
        GameManager.Instance.GameLost -= OnGameLost;
    }

    private void OnWeaponFired(int index, Weapon weapon)
    {
        _countPairs[index].Count.text = weapon.CurrentRound + "/" + weapon.MaxRound;
    }

    private void OnWeaponAdded(int index, Weapon weapon)
    {
        LoadIcon(index, weapon);
    }

    private void OnWeaponSwitched(int index, Weapon weapon)
    {
        foreach (var pair in _countPairs)
        {
            pair.Icon.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        _countPairs[index].Icon.color = Color.white;
        _countPairs[index].Count.text = weapon.CurrentRound + "/" + weapon.MaxRound;
    }

    private void OnGameLost()
    {
        gameObject.SetActive(false);
    }

    private void LoadIcon(int index, Weapon weapon)
    {
        var pair = _countPairs[index];
        
        pair.Count.enabled = true;
        pair.Count.text = weapon.CurrentRound + "/" + weapon.MaxRound;
        
        pair.Icon.enabled = true;
        pair.Icon.sprite = weapon.PrefabReference.GetComponent<SpriteRenderer>().sprite;
        pair.Icon.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    }
}