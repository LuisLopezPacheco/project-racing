using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Crear instancia
    private Vector3 respawnPosition; // Almacena la posición para respawnear
    public GameObject _deatEffect; //Efecto al morir


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerController.instance.transform.position; //guardar la posición del jugador al iniciar
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        StartCoroutine("RespawnWaiter");
    }

    public IEnumerator RespawnWaiter()
    {
        //Función Instantiate permite aparecer a los objetos en el mundo
        //Activar las particulas obteniendo la posición y rotación del personaje
        _deatEffect.SetActive(true);
        Instantiate(_deatEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);
        //Desactivar personaje
        foreach (GameObject playerPiece in PlayerController.instance._playerPieces)
        {
            //si es par el jugador se muestra, y si no el jugador no se muestra
            playerPiece.SetActive(false);
            
        }
        UIManager.instance._fadeToBlack = true;
        yield return new WaitForSeconds(2f);
        UIManager.instance._fadeFromBlack = true;
        //Cambiar posición
        PlayerController.instance.transform.position = respawnPosition;
        //Volver activar personaje
        foreach (GameObject playerPiece in PlayerController.instance._playerPieces)
        {
            //si es par el jugador se muestra, y si no el jugador no se muestra
            playerPiece.SetActive(true);

        }
        HealthManager.instance.ResetHealth(); //Resetear la vida
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("span set");
    }
}
