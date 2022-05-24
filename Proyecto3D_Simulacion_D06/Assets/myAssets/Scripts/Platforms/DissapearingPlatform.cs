using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour
{
    [SerializeField]

    private GameObject _parent;
    private BoxCollider _platformBase;
    private Renderer _platformRender;
    private GameObject _realPlatform;
    private float _speed = 10f; //10 metros por segundo
    private bool _canDetect;

    public void Start()
    {
        _realPlatform = transform.parent.gameObject; //PrataformaFísica (Desaparecer)
        _platformBase = _realPlatform.GetComponent<BoxCollider>(); //Dejar caer al jugador (Desactivar)
        _platformRender = _realPlatform.GetComponent<Renderer>();//Cambiar color de material a invisible
        _canDetect = true; //Puede detectar al jugador
    }
   
    public void OnTriggerEnter(Collider other) // other es el jugador
    {
        if (_canDetect)
        {
            _canDetect = false;
            StartCoroutine("Dissapear");
        }
    }


    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Salió" + other);
        other.transform.parent = null;// Excluímos como hijo de la plataforma a cualquier objeto que se separe de ella
    }

    IEnumerator Dissapear()
    {
        Color c = _platformRender.material.color;//Guardar color del material
        for(float x = 1f; x>= 0.1f; x -= 0.1f)   //10 veces
        {
            c.a = x; //equivalente a alfa (transparencia), camibar el alfa poco a poco
            _platformRender.material.color = c;
            yield return new WaitForSeconds(.1f);
        }
        _platformBase.enabled = false;  //quitar el box colider de la plataforma (Desactivar colisionador para que el personaje caiga
        yield return new WaitForSeconds(2f);    // esperar dos segundos, teimpo en que la plataforma es invisible
        for(float x = 0f; x <= 1f; x+=0.1f)//10 veces
        {
            c.a = x;
            _platformRender.material.color = c;
            yield return new WaitForSeconds(.05f);  //Espera menos para hacer el cambio
        }
        _platformBase.enabled = true; //Activar el colisionador para que el jugador pueda pisarlo
        //Activo el box collider
        _canDetect = true; //Volver a detectar para hacer invisible
    }
}
