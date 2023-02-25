using System;
using UnityEngine;

[Serializable]
public class PlayerLook
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _xSensitivity;
    [SerializeField] private float _ySensitivity;
    private float _xRotation = 0f;
    private Transform PlayerCameraTransform => _playerCamera.transform;
    private Transform PlayerTransform => _playerController.transform;
    public void LookWithMouse(Vector2 mouseLookDelta)
    {
        _xRotation -= (mouseLookDelta.y * Time.deltaTime) * _ySensitivity;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

        PlayerCameraTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        PlayerTransform.Rotate(Vector3.up * (mouseLookDelta.x * Time.deltaTime) * _xSensitivity);
    }
}
