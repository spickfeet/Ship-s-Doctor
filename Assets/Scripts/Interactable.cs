using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour, IInteractable
{
    protected Player _player;

    public abstract void Interact();

    public void Inject(Player player)
    {
        _player = player;
    }
}
