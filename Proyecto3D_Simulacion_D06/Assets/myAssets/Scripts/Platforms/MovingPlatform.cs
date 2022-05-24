using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private Transform _WPA, _WPB;
    private Transform _destination;
    private Transform _realPlatform;
    private float _speed = 10f; //10 metros por segundo
    private bool _detect;

    public void Start()
    {
        _realPlatform = transform.parent; //Tomando la plataforma física
        _destination = _WPB; //El lugar a donde será desplazada
        _detect = true; //Puede detectar al jugador
    }

    public void OnTriggerEnter(Collider other) // other es el jugador
    {
        Debug.Log("Entró" + other);
        // Revisar si es tag del player
        other.transform.parent = transform; // Hacer que el padre del jugador sea el detector 'trigger'
        if (_detect)
        {
            _detect = false;
            StartCoroutine("Movimiento");
        }
    }


    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Salió" + other);
        other.transform.parent = null;// Excluímos como hijo de la plataforma a cualquier objeto que se separe de ella
    }

    IEnumerator Movimiento()
    {
        Debug.Log("Moviendo");
        while (_realPlatform.position != _destination.position)
        {
            _realPlatform.position = Vector3.MoveTowards(_realPlatform.position, _destination.position, Time.deltaTime * _speed);
            yield return null;
        }
        if (_destination == _WPA)
        {
            _destination = _WPB;
        }
        else if (_destination == _WPB)
        {
            _destination = _WPA;
        }
        _detect = true;
    }
}
