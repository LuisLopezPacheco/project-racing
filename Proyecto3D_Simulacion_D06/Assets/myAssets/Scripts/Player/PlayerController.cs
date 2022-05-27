using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
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

    #region Barravida y Vida
    [SerializeField]
    private Image _barravida;
    [SerializeField]
    private float _hp, _maxHp = 40f;
    #endregion



    private int _puntos;

    public bool _isKnocking; //activar y desactivar Knockback
    public float _knockBackLength = .5f;
    private float _knockBackCounter;
    public Vector2 _knockBackPower;

    #region Puntaje
    private int _coins;
    public TextMeshProUGUI scoreText;
    #endregion

    public GameObject[] _playerPieces;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _coins = 0;

        #region Vida jugador
        _hp = _maxHp;
        //_barravida.fillAmount = _hp/_maxHp;
        #endregion

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
        if (!_isKnocking)
        {
            _horizontalInput = Input.GetAxis("Horizontal"); //AD izquierda derecha
            _forwardInput = Input.GetAxis("Vertical"); //WS arriba abajo

            float velocity = Mathf.Max(Mathf.Abs(_horizontalInput), Mathf.Abs(_forwardInput));
            velocity = velocity * _speed / _maxSpeed;
            _playerAnim.setSpeed(velocity);

            Vector3 movement = new Vector3(_horizontalInput, 0, _forwardInput);
            transform.Translate(movement * _speed * Time.deltaTime);
        }

        if (_isKnocking)
        {
            _knockBackCounter -= Time.deltaTime;
            //float yStore = moveDirection.y;
            //moveDirection = (transform.forward * _knockBackPower.x);
            //moveDirection.y = yStore;
            //charController


            if (_knockBackCounter <= 0)
            {
                _isKnocking = false;
            }
        }
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
        else if (collider.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Win");
        }
        
        
    }

    public void KnockBack()
    {
        _isKnocking = true;
        _knockBackCounter = _knockBackLength;
        
    }

    public void setCoins(int coin)
    {
        _coins += coin;
        Debug.Log("El jugador tiene: " + _coins + " coins");
        //Cambiar el valor del TextMeshPro cada que se agarre una moneda 
        scoreText.text = "Score: " + _coins.ToString();
    }

}

