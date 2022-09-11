using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuToggle : MonoBehaviour
{

    [SerializeField] private InputActionReference menuToggle;
    [SerializeField] private GameObject toggleUI;
    
    void Start()
    {
        menuToggle.action.performed += OnMenuPressed;
    }

    private void OnMenuPressed(InputAction.CallbackContext obj)
    {
        toggleUI.SetActive(!toggleUI.activeSelf);
    }

    
}
