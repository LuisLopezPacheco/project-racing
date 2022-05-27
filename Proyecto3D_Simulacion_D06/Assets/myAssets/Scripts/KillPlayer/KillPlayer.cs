using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HealthManager.instance._lifes--;
            if (HealthManager.instance._lifes <= 0)
            {
                GameManager.instance.GameOver();
                Debug.Log("GAME OVER");
                //GAME OVER
            }
            else
            {
                Debug.Log("ZONA KILL");
                GameManager.instance.Respawn();
            }
        }
        
    }
}
