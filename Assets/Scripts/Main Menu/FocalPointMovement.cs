using UnityEngine;

public class FocalPointMovement : MonoBehaviour
{
    [SerializeField] private float rotationSensitivity;

    private float _yRotation;
    private float _xRotation;

    private void Update()
    {
        RotateCamera();
    }

    /// <summary>
    /// Rotates camera around focal point
    /// </summary>
    private void RotateCamera()
    {        
        if (Input.GetMouseButton(0))
        {
            float mousePositionX = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
            float mousePositionY = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;

            _yRotation += mousePositionX;
            _xRotation -= mousePositionY;

            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        }
    }
}
