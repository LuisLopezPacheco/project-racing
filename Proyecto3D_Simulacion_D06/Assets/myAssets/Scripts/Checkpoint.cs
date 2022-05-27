using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject _checkPointON, _checkPointOFF;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.SetSpawnPoint(transform.position);

            Checkpoint[] _allCheckPoint = FindObjectsOfType<Checkpoint>();
            for (int i = 0; i < _allCheckPoint.Length; i++)
            {
                _allCheckPoint[i]._checkPointOFF.SetActive(true); //desactivar checkpoints
                _allCheckPoint[i]._checkPointON.SetActive(false);
            }
            //desactivar checkpoints para dejar activo solo el actual
            _checkPointOFF.SetActive(false);
            _checkPointON.SetActive(true);
        }

    }
}
