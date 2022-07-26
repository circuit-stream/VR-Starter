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

    [SerializeField]
    private float AnimationLerpFraction = 0.2f;

    float TouchPosition=0;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        TouchPosition = Mathf.Lerp(TouchPosition, point.action.ReadValue<float>(), AnimationLerpFraction);

        animator.SetFloat("Select", flex.action.ReadValue<float>());
        animator.SetFloat("TriggerTouch", TouchPosition);
    }
}
