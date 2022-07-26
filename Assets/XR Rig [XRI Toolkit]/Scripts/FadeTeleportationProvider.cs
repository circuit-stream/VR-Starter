using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class FadeTeleportationProvider : TeleportationProvider
{
    [SerializeField] private bool EnableFade = true;
    [SerializeField] private RawImage faderImage;
    [SerializeField] private float fadeSpeed = 0.01f;

    float timer = 0;

    private void Start()
    {
        faderImage.color = Color.clear;
    }

    IEnumerator FadeIn(TeleportRequest teleportRequest)
    {
        timer = 0;

        while (timer <= 1)
        {
            faderImage.color = Color.Lerp(Color.clear, Color.black, timer);
            timer += fadeSpeed;
            yield return new WaitForEndOfFrame();
        }

        faderImage.color = Color.black;

        currentRequest = teleportRequest;
        validRequest = true;
    }

    IEnumerator FadeOut()
    {
        timer = 0;

        while (timer <= 1)
        {
            faderImage.color = Color.Lerp(Color.black, Color.clear, timer);
            timer += fadeSpeed;
            yield return new WaitForEndOfFrame();
        }

        faderImage.color = Color.clear;

        EndLocomotion();
    }

    public override bool QueueTeleportRequest(TeleportRequest teleportRequest)
    {
        if(EnableFade)
            StartCoroutine(FadeIn(teleportRequest));
        else
        {
            currentRequest = teleportRequest;
            validRequest = true;
        }

        return true;
    }


    protected override void Update()
    {
        if (!validRequest || !BeginLocomotion())
            return;

        var xrOrigin = system.xrOrigin;
        if (xrOrigin != null)
        {
            switch (currentRequest.matchOrientation)
            {
                case MatchOrientation.WorldSpaceUp:
                    xrOrigin.MatchOriginUp(Vector3.up);
                    break;
                case MatchOrientation.TargetUp:
                    xrOrigin.MatchOriginUp(currentRequest.destinationRotation * Vector3.up);
                    break;
                case MatchOrientation.TargetUpAndForward:
                    xrOrigin.MatchOriginUpCameraForward(currentRequest.destinationRotation * Vector3.up, currentRequest.destinationRotation * Vector3.forward);
                    break;
                case MatchOrientation.None:
                    // Change nothing. Maintain current origin rotation.
                    break;
                default:
                    Assert.IsTrue(false, $"Unhandled {nameof(MatchOrientation)}={currentRequest.matchOrientation}.");
                    break;
            }

            var heightAdjustment = xrOrigin.Origin.transform.up * xrOrigin.CameraInOriginSpaceHeight;

            var cameraDestination = currentRequest.destinationPosition + heightAdjustment;

            xrOrigin.MoveCameraToWorldLocation(cameraDestination);
        }

        if(EnableFade)
            StartCoroutine(FadeOut()); // Fade out and endlocomotion
        else
            EndLocomotion();

        validRequest = false;
    }
}
