using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int _currentHealth, _maxHealth;
    public float _invincibleLength = 2f;
    private float _invincCounter;   //hacer conteo del tiempo de la invincibilidad

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        UpdateUI(); //Actualizar la vida del jugador
    }

    // Update is called once per frame
    void Update()
    {
        //Validar el tiempo que el jugardor es inmune
        if(_invincCounter > 0)
        {
            _invincCounter -= Time.deltaTime;
            foreach (GameObject playerPiece in  PlayerController.instance._playerPieces)
            {
                //si es par el jugador se muestra, y si no el jugador no se muestra
                if (Mathf.Floor(_invincCounter * 5f)%2 == 0)
                {
                    playerPiece.SetActive(true);
                }
                else
                {
                    playerPiece.SetActive(false);
                }

                if(_invincCounter <= 0)
                {
                    playerPiece.SetActive(true);
                }
            }
            
        }
    }

    //recibir daño
    public void Hurt()
    {
        if(_invincCounter <= 0)
        {
            _currentHealth--;
            //Validar si la vida llega a cero
            if(_currentHealth <= 0)
            {
                _currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerController.instance.KnockBack();
                _invincCounter = _invincibleLength; //activar inmunidad
            }
        }
        UpdateUI(); //Actualizar la vida del jugador
    }

    //Resetear vida al morir 
    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        UpdateUI(); //Actualizar la vida del jugador
    }

    //añadir vida al jugador
    public void AddHealth(int amountToHeal)
    {
        _currentHealth += amountToHeal; //añadir vida
        if(_currentHealth > _maxHealth) 
        {
            _currentHealth = _maxHealth;
        }
        UpdateUI(); //Actualizar la vida del jugador
    }

    public void UpdateUI()
    {
        UIManager.instance.healhText.text = _currentHealth.ToString();
    }
}
