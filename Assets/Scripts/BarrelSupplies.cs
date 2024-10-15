using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BarrelSupplies : Interactable
{
    [SerializeField] private ItemType _itemType;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        Item item = _player.Inventory.GetItem(_itemType);

        if (item.Count == _player.MaxItems) return;

        while (item.Count < _player.MaxItems)
        {
            _player.Inventory.AddItem(_itemType);
        }
        if (item.Count == _player.MaxItems)
        {
            _audioSource.Play();
        }
    }
}