using Cinemachine;
using System.Collections;
using UnityEngine;

public class HoldLadder : Interactable
{
    [SerializeField] private Transform _holdPos;
    [SerializeField] private Transform _shipPos;
    [SerializeField] private bool _deck;

    private Camera _camera;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private Vector3 _offset = new Vector3(0, 0, -10);

    private void Start()
    {
        _camera = FindAnyObjectByType<Camera>();
        _cinemachineVirtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
    }

    public override void Interact()
    {
        switch (_deck)
        {
            case true:
                StartCoroutine(CinemachineVirtualCameraEnable());
                _camera.transform.position = _holdPos.position + _offset;
                _player.transform.position = _holdPos.position;
                break;
            case false:
                StartCoroutine(CinemachineVirtualCameraEnable());
                _camera.transform.position = _shipPos.position + _offset;
                _player.transform.position = _shipPos.position;
                break;
        }
    }
    private IEnumerator CinemachineVirtualCameraEnable()
    {
        _cinemachineVirtualCamera.enabled = false;
        yield return new WaitForSeconds(0.01f);
        _cinemachineVirtualCamera.enabled = true;

    }
}
