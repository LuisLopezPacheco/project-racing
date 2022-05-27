using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private GameObject[] _objetos; // Se inicializa en el inspector de unity (Vector de objetos de juego)
#pragma warning restore 0649
    [SerializeField]
    private Vector3 _spawnValues; // Coordenadas X,Y,Z
    private float _spawnWait; // Cuanto va a pasar antes del primer objeto
    private float _spawnWaitMax; // tiempo máximo para el random de spawn
    private float _spawnWaitMin; // tiempo mínimo para el random de spawn
    private static bool _stop; // Parar todos los spawners
    private int _randObject; // Objeto aleatorio a instanciar
    private int _maxObjects; // Número máximo de objetos a crear
    private static int _spawnedObjects;  // Número actual de objetos creados

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitSpawner());
        _spawnedObjects = 0;
        _spawnWaitMin = 1;
        _spawnWaitMax = 4;
        _stop = false;
        _maxObjects = 5;
        _spawnedObjects = 0;
        _spawnValues = new Vector3(5, transform.position.y, 5);  // X, Y, Z
    }

    // Update is called once per frame
    void Update()
    {
        _spawnWait = Random.Range(_spawnWaitMin, _spawnWaitMax); // Puede estar dentro del IEnumerator
    }

    IEnumerator WaitSpawner()
    {
        yield return new WaitForSeconds(3); //pasan 3 segundos antes de comenzar
        while (!_stop && _spawnedObjects < _maxObjects)
        {
            _randObject = Random.Range(0, _objetos.Length);  // Selecciona objeto al azar
            // Conseguir la posición al azar entre un rango predeterminado
            Vector3 spawnPosition = new Vector3(Random.Range(-_spawnValues.x, _spawnValues.x), _spawnValues.y, Random.Range(-_spawnValues.z, _spawnValues.z));
            // Línea importante, se intancia el objeto
            //Revisar posición para no colocar objetos en la posición de otro
            GameObject InstantiatedGameObject = Instantiate(_objetos[_randObject], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation); // Quaternion.identity
            // El nuevo objeto va a ser hijo del spawner (evitar aglomeración en el hierarchy)
            InstantiatedGameObject.transform.SetParent(transform);
            // Aumenta número de objetos creados
            _spawnedObjects++;
            // Esperar _spawnWait antes de crear otro objeto
            yield return new WaitForSeconds(_spawnWait);
        }
    }

    public void StopSpawner()
    {
        _stop = true;
    }
}

