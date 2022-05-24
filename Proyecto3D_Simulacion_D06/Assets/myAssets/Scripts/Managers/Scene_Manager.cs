using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    private void Update()
    {
        
    }
}
