using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _interactRadius = 2f;

    [SerializeField] private int _maxItems = 6;
    public int MaxItems => _maxItems;

    [SerializeField] private GameObject _interactButton;

    [SerializeField] private LayerMask _layerMask;

    private Inventory _inventory;
    public Inventory Inventory => _inventory;


    public void Inject(Inventory inventory)
    {
        _inventory = inventory;
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _interactRadius, _layerMask);

        if (colliders.Length <= 0)
        {
            if (_interactButton.activeSelf == false) return;

            _interactButton.SetActive(false);

            return;
        }

        if (_interactButton.activeSelf == false)
        {
            _interactButton.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact(colliders);
        }
    }

    private void Interact(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Interactable interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _interactRadius);
    }
}
