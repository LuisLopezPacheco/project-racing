using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        if (_playerAnimator == null)
        {
            Debug.LogWarning("El hijo del jugador no tiene un componente Animator");
        }
        
    }

    public void setSpeed(float speed)
    {
        _playerAnimator.SetFloat("Speed", speed);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
