using UnityEngine;

public class PropellerRotator : MonoBehaviour
{
    private Transform _propeller;

    private void Start()
    {
        _propeller = transform;
    }

    private void Update()
    {
        RotatePropeller();
    }

    private void RotatePropeller()
    {
        float rotationSpeed = 5;
        _propeller.Rotate(Vector3.left * rotationSpeed);
    }
} 