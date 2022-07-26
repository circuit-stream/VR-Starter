using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetGrabInteractable : XRGrabInteractable
{
    [SerializeField] private bool OffsetGrab = false;

    private Pose initialPose = new Pose();

    private void Start()
    {
        // Save the initial pose
        initialPose.position = attachTransform.position;
        initialPose.rotation = attachTransform.rotation;
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (OffsetGrab)
        {
            attachTransform.position = args.interactorObject.GetAttachTransform(args.interactableObject).position;
            attachTransform.rotation = args.interactorObject.GetAttachTransform(args.interactableObject).rotation;
        }

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        if(OffsetGrab)
        {
            // Reset the attach transform to its original
            attachTransform.position = initialPose.position;
            attachTransform.rotation = initialPose.rotation;
        }

        base.OnSelectExiting(args);
    }

}
