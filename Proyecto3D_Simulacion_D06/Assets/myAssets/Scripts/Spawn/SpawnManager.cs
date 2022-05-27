using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _coin;
    [SerializeField]
    private int _xPos, _yPos, _zPos, _maxCoin;
    private int _countCoin;

    private void Start()
    {
        _maxCoin = 20;
        _countCoin = 0;

        //Llamar a la función como Coroutine
        StartCoroutine(CoinsDrop());
    }

    IEnumerator CoinsDrop()
    {
        while (_countCoin < _maxCoin)
        {
            //Crear posiciones aleatorias 
            _xPos = Random.Range(-22, 22);
            _yPos = Random.Range(5, 10);
            _zPos = Random.Range(-22, 22);
            //Instanciar el objeto
            GameObject InstantiatedGameObject = Instantiate(_coin, new Vector3(_xPos, _yPos, _zPos), Quaternion.Euler(0, 0, 90));

            //Que sea padre del objeto para evitar que salgan muchos en la jerarquía
            InstantiatedGameObject.transform.SetParent(transform);
            yield return new WaitForSeconds(0.1f);
            _countCoin++;
        }
    }
}
