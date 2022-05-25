using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _WPA;

    private void OnTriggerEnter(Collider other)
    {
        //Normalmente el origen de la plataforma esta en el centro
        Vector3 distBetween = _WPA.position - transform.position; //Hacer que se coloque encima de la plataforma
        other.transform.position += distBetween; //asignar la posición de la plataforma al jugador
    }
}
