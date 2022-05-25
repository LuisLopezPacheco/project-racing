using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : Interactable
{
    #region variables NPC
    [SerializeField]
    private string _name = "Bobby el puente";
    [SerializeField]
    private string[] _dialogue;
    #endregion

    private DialogueManager _dialogueManager;

    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();//Para singletons
        if (_dialogueManager == null)
        {
            Debug.LogError("No hay un script DialogueManager en la escena");
        }
    }


    public override void Interact(PlayerController player)
    {
        Debug.Log("Interactuando con el NPC " + _name);
        _dialogueManager.SetDialogue(_name, _dialogue);
    }
}




