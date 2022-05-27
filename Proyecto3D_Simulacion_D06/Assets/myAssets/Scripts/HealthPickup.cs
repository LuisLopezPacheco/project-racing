using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int _healAmount; //Cuanta vida dar al jugador
    public bool _isFullHeal; //decidir si  dará toda la vida

    public GameObject _healthEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject); //desaparecer objeto
            Instantiate(_healthEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);

            if (_isFullHeal)
            {
                HealthManager.instance.ResetHealth();
            }
            else
            {
                HealthManager.instance.AddHealth(_healAmount);
            }
        }
    }
}
