using System.Collections;
using System.Collections.Generic;
/*using TreeEditor;*/
using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier;
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;

    private float accumulatedDeltaX = 0f;
    private int frameCount = 0;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;

    }

    // Update is called once per frame
    

    void Update()
    {
        float deltaX = cameraTransform.position.x - previousCameraPosition.x;
        accumulatedDeltaX += deltaX;
        frameCount++;

        if (frameCount >= 3) // Ajusta el número de frames según tus necesidades
        {
            float averageDeltaX = accumulatedDeltaX / frameCount;
            transform.Translate(new Vector3(averageDeltaX * parallaxMultiplier, 0, 0));
            accumulatedDeltaX = 0f;
            frameCount = 0;
        }

        previousCameraPosition = cameraTransform.position;
    }
}
