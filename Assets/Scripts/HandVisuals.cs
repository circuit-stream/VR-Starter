using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class HandVisuals : MonoBehaviour
{
    protected Animator animator;

    [SerializeField]
    private InputActionProperty flex;

    [SerializeField]
    private InputActionProperty point;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        animator.SetFloat("ControllerSelectValue", flex.action.ReadValue<float>());
        animator.SetFloat("ControllerActivateValue", point.action.ReadValue<float>());
    }
}
