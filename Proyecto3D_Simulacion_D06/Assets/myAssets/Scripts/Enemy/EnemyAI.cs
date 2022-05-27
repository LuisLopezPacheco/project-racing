using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _objetive;
    private float _speed = 4f, _dist2Objv, _angle2Objv;
    private Animator _enemyAnim;
    private float _lastAttack, _attackCoolDown = 2f;
    private bool _canAttack;
    private int _pts = 0;
    [SerializeField]
    private Transform _enemyCollisionDetector;
    private float _colliderRadius = 0.5f;
    private Vector3 EnemyOffset;
    // Start is called before the first frame update
    void Start()
    {
        _canAttack = false;
        _objetive = GameObject.FindGameObjectWithTag("Player");
        if(_objetive == null)
        {
            Debug.LogError("No hay un objectivo con la Tag nombrada");
        }
        _enemyAnim = GetComponent<Animator>();
        _enemyCollisionDetector = transform.GetChild(0);
        EnemyOffset = new Vector3(0, 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (LookingAtPlayer()) //Llamar a la función
        {
            FollowPlayer();
        }
        else
        {
            BlindSearch();
        }
    }

    //Función para rotar cuando se tope con la pared
    private void BlindSearch()
    {
        //Mediante una posición crea un radio 
        if(Physics.CheckSphere(_enemyCollisionDetector.position + EnemyOffset, _colliderRadius, 1 << 3))
        {
            if (Time.time - _lastAttack > _attackCoolDown)
            {
                if (Random.value > 0.5f)
                {
                    transform.Rotate(0, 50, 0);
                }
                else
                {                                      
                    transform.Rotate(0, -50, 0);                   
                }
            }             
        }
        else
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }

    //Función para que siga al jugador
    private void FollowPlayer()
    {
        Vector3 groundedObjective = new Vector3(_objetive.transform.position.x, transform.position.y, _objetive.transform.position.z);
        transform.LookAt(groundedObjective);               
        if (_dist2Objv <= 1) //Ataca y deja de moverse
        {
            _canAttack = true;
            //Hacer el Daño al Player
            if(Time.time - _lastAttack > _attackCoolDown)
            {
                _enemyAnim.SetTrigger("Attack");
                _lastAttack = Time.time;
            }
            _pts += 2;
        }
        else
        {
            //Mover al enemigo hacia adelante 
            _canAttack = false;
            transform.position += transform.forward * _speed * Time.deltaTime;                        
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_canAttack)
        {
            Debug.Log("Atacando" + _pts.ToString());
        }        
    }

    //Función para revisar si el jugador esta cerca 
    private bool LookingAtPlayer()
    {
        //Se calcula la distancia del jugador y enemigo
        _dist2Objv = Vector3.Distance(_objetive.transform.position, transform.position);
        //Conseguir el Angulo de visión 
        _angle2Objv = Vector3.Angle(_objetive.transform.position - transform.position, transform.forward);

        //Saber a que distancia y angulo el enemigo lo reconoce o sabe de su existencia del jugador              
        if ((_angle2Objv <= 45 && _dist2Objv <= 10) || _dist2Objv <=3 ) //Detecta
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
