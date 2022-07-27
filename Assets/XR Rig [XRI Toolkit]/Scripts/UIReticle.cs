using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class UIReticle : MonoBehaviour
{
    [SerializeField] private GameObject reticlePrefab;

    [SerializeField] private XRInteractorLineVisual[] lines;

    private GameObject reticle;

    private void Start()
    {
        foreach (XRInteractorLineVisual line in lines)
        {
            reticle = Instantiate(reticlePrefab);
            line.reticle = reticle;
        }
    }

    


}

