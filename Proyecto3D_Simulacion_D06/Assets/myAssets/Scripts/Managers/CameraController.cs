using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Componentes de la cámara
    [SerializeField]
    private Transform _player, _playerCamera, _focusPoint;
    [SerializeField]
    private float _cameraHeight = 5f;
    #endregion

    #region Variables zoom
    [SerializeField]
    private float _zoom = -5f;
    [SerializeField]
    private float _zoomSpeed = 8f;
    [SerializeField]
    private float _zoomMax = 0f, _zoomMin = -10f;
    #endregion

    #region Variables rotación
    [SerializeField]
    private float _camRotX, _camRotY;
    [SerializeField]
    private float _mouseSensitivity = 5;
    #endregion

    void Start()
    {
        #region Comprobar la asignación de componentes
        if (_player == null)
        {
            Debug.Log("El jugador no se asignó al CameraController");
        }
        if (_playerCamera == null)
        {
            Debug.Log("La cámara no se asignó al CameraController");
        }
        if (_focusPoint == null)
        {
            Debug.Log("El punto focal no se asignó al CameraController");
        }
        #endregion

        #region Asignar parentesco
        _focusPoint.SetParent(_player);
        _playerCamera.SetParent(_focusPoint);
        #endregion

        #region Asignar pos,rot,scale
        _focusPoint.localPosition = new Vector3(0, _cameraHeight, 0);
        _focusPoint.localRotation = Quaternion.Euler(0, 0, 0);
        _focusPoint.localScale = new Vector3(1, 1, 1);
        _playerCamera.localPosition = new Vector3(0, 0, _zoom);
        _playerCamera.LookAt(_player);
        _playerCamera.localScale = new Vector3(1, 1, 1);
        #endregion
    }

    void Update()
    {
        #region Zoom
        _zoom += Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
        _zoom = Mathf.Clamp(_zoom, _zoomMin, _zoomMax);
        #endregion

        #region Rotación
        if (Input.GetMouseButton(1))
        {
            _camRotX += Input.GetAxis("Mouse X") * _mouseSensitivity;
            _camRotY += Input.GetAxis("Mouse Y") * _mouseSensitivity;//-60 a 89
            _camRotY = Mathf.Clamp(_camRotY, -60f, 89.5f);
            _focusPoint.localRotation = Quaternion.Euler(_camRotY, 0, 0);
            _player.localRotation = Quaternion.Euler(0, _camRotX, 0);
        }
        #endregion

        #region Actualizar cámara
        _playerCamera.localPosition = new Vector3(0, 0, _zoom);
        _playerCamera.LookAt(_player);
        #endregion
    }
}
