using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRB;

    #region Variables Movimiento
    [SerializeField]
    private float _maxSpeed = 7f;
    private float _speed;
    private bool isRunning;
    private float _horizontalInput, _forwardInput;
    #endregion

    #region Variables Brinco
    private bool _jumpRequest = false;
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private int _availableJumps = 0, _maxJumps = 2;
    #endregion

    #region Animación
    private PlayerAnimation _playerAnim;
    #endregion

    private int _puntos;

    void Start()
    {
        #region Obtener Rigidbody
        _playerRB = GetComponent<Rigidbody>();
        if (_playerRB == null)
        {
            Debug.LogWarning("El jugador no tiene cuerpo rígido");
        }
        #endregion

        #region Obtener player animation
        _playerAnim = GetComponent<PlayerAnimation>();
        if (_playerAnim == null)
        {
            Debug.LogWarning("El jugador no tiene el script PlayerAnimation");
        }
        #endregion

        _speed = _maxSpeed;
        isRunning = true;

    }

    void Update()
    {
        #region Revisar si corre o camina
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = !isRunning;
            if (isRunning)
            {
                _speed = _maxSpeed;
            }
            else
            {
                _speed = _maxSpeed / 2;
            }
        }
        #endregion


        #region Movimiento
        _horizontalInput = Input.GetAxis("Horizontal"); //AD izquierda derecha
        _forwardInput = Input.GetAxis("Vertical"); //WS arriba abajo

        float velocity = Mathf.Max(Mathf.Abs(_horizontalInput), Mathf.Abs(_forwardInput));
        velocity = velocity * _speed / _maxSpeed;
        _playerAnim.setSpeed(velocity);

        Vector3 movement = new Vector3(_horizontalInput, 0, _forwardInput);
        transform.Translate(movement * _speed * Time.deltaTime);
        #endregion

        #region Salto
        if (Input.GetKeyDown(KeyCode.Space) && _availableJumps > 0)
        {
            _jumpRequest = true;
        }
        #endregion
    }

    private void FixedUpdate()
    {
        if (_jumpRequest)
        {
            _playerRB.velocity = Vector3.up * _jumpForce;
            _availableJumps--;
            _jumpRequest = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _availableJumps = _maxJumps;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Entra al trigger");
        if (collider.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("Se encontró un objeto para interactuar");
            Interactable interacted = collider.GetComponent<Interactable>();
            if (interacted == null)
            {
                Debug.Log("Pero no es de tipo Interactable");
            }
            else
            {
                interacted.Interact(this);
            }
        }
    }

    
}

