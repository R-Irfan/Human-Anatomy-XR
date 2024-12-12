using UnityEngine;

public class PositionBeforePlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PositionObjectInFrontOfCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PositionObjectInFrontOfCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 newPosition = mainCamera.transform.position + mainCamera.transform.forward;
            newPosition.y = 0; // Set y position to 0
            transform.position = newPosition;
        }
    }
}
