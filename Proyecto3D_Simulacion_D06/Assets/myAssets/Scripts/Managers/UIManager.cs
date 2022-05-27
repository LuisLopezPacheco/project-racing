using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    public Image _BlackScreen;
    public float _fadeSpeed;
    public bool _fadeToBlack, _fadeFromBlack;

    public Text healhText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_fadeToBlack)
        {
            _BlackScreen.color = new Color(_BlackScreen.color.r, _BlackScreen.color.g, _BlackScreen.color.b, Mathf.MoveTowards(_BlackScreen.color.a, 1f, _fadeSpeed* Time.deltaTime));
            if(_BlackScreen.color.a == 1f)
            {
                _fadeToBlack = false;
            }
        }
        if (_fadeFromBlack)
        {
            _BlackScreen.color = new Color(_BlackScreen.color.r, _BlackScreen.color.g, _BlackScreen.color.b, Mathf.MoveTowards(_BlackScreen.color.a, 0f, _fadeSpeed * Time.deltaTime));
            if (_BlackScreen.color.a == 0f)
            {
                _fadeFromBlack = false;
            }
        }
    }
}
