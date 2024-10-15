using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, _cameraTarget.position + _offset, Time.deltaTime * _speed);
    }
}
