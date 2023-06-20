using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier;  //Move Speed
    private Transform cameraTransform;  //Camera Position
    private Vector3 lastCameraPosition;

    public GameObject myBG;
    bool isHere;

    private void Start()
    {
        cameraTransform = UnityEngine.Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        isHere = true;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += deltaMovement * parallaxEffectMultiplier;
        lastCameraPosition = cameraTransform.position;

        if (Input.GetKeyDown(KeyCode.U))
        {
            isHere = !isHere;
            myBG.SetActive(isHere);
        }
    }
}
