using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    #region Variables del panel de diálogos
    [SerializeField]
    private GameObject _dialoguePnl;

    private Button _nextBttn;
    private TextMeshProUGUI _dialogueTxt, _nameTxt, _nextTxt;
    #endregion

    #region Variables NPC
    private string _name;
    private List<string> _dialogueList;
    private int _dialogueIdx;
    #endregion



    private void Start()
    {
        _dialoguePnl.SetActive(false);
        #region Obtener componentes del panel de diálogos
        if (_dialoguePnl == null)
        {
            Debug.LogWarning("No se asignó el panel de diálogos al manejador");
        }
        else
        {
            #region Obtener texto de diálogo
            //_dialogueTxt = _dialoguePnl.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _dialogueTxt = _dialoguePnl.GetComponentInChildren<TextMeshProUGUI>();
            if (_dialogueTxt == null)
            {
                Debug.LogError("El panel de diálogo no tiene un primer hijo TMP");
            }
            //else
            //{
            //    _dialogueTxt.text = "Diálogos inicializados";
            //}
            #endregion
            #region Obtener texto de nombre NPC
            //Buscar el componente TMP en el primer hijo del segundo hijo del panel
            _nameTxt = _dialoguePnl.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
            if (_nameTxt == null)
            {
                Debug.LogError("No hay un TMP como primer hijo del segundo hijo del panel");
            }
            //else
            //{
            //    _nameTxt.text = "Nombre inicializado";
            //}
            #endregion
            #region Obtener el botón y poner listener
            //Buscar el componente Button en el tercer hijo del panel
            _nextBttn = _dialoguePnl.transform.GetChild(2).GetComponent<Button>();
            if (_nextBttn == null)
            {
                Debug.LogWarning("No hay un botón como tercer hijo del panel de diálogo");
            }
            else
            {
                _nextBttn.onClick.AddListener(delegate { ContinueDialogue(); });
                #region Obtener Texto del botón
                _nextTxt = _nextBttn.GetComponentInChildren<TextMeshProUGUI>();
                if (_nextTxt == null)
                {
                    Debug.LogWarning("El botón de continuar no tiene texto");
                }
                //else
                //{
                //    _nextTxt.text = "Botón inicializado";
                //}
                #endregion
            }
            #endregion
        }
        #endregion
       // _dialoguePnl.SetActive(false);
    }

    public void SetDialogue(string name, string[] dialogue)
    {
        _name = name;
        _dialogueList = new List<string>(dialogue.Length);
        _dialogueList.AddRange(dialogue);
        _dialogueIdx = 0;
        //_dialogueTxt.text = _dialogueList[_dialogueIdx];
        ShowDialogue();
        _nameTxt.text = _name;
        _nextTxt.text = "Continuar";
        _dialoguePnl.SetActive(true);
    }

    private void ShowDialogue()
    {
        Debug.Log("Diálogo #" + _dialogueIdx);
        _dialogueTxt.text = _dialogueList[_dialogueIdx];
    }

    private void ContinueDialogue()
    {
        if (_dialogueIdx == _dialogueList.Count - 1)//Se terminó la conversación
        {
            Debug.Log("Termina conversación");
            _dialoguePnl.SetActive(false);
        }
        else if (_dialogueIdx == _dialogueList.Count - 2)//Último díalogo
        {
            _dialogueIdx++;
            ShowDialogue();
            _nextTxt.text = "Salir";
        }
        else
        {
            _dialogueIdx++;
            ShowDialogue();
        }

    }

}

