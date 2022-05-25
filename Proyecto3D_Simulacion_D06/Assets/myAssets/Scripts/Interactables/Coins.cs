using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Interactable
{
    [SerializeField]
    private int _coins = 1;
    public override void Interact(PlayerController player)
    {
        //Dar puntos al jugador
        player.setCoins(_coins);
        //Destruir el objeto que tenga este script
        Destroy(gameObject);
       
    }
}
