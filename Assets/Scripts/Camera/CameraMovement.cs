using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] Vector3 normalPosition;
    [SerializeField] Vector3 wheelSpinPosition;

    [Header("Rotation")]
    [SerializeField] Quaternion normalRotation;
    [SerializeField] Quaternion wheelSpinRotation;

    [Header("Speeds")]
    [SerializeField] float moveSpeed = 1;

    public void MoveForWheelSpin()
    {
        StartCoroutine(RotateTo(wheelSpinRotation));

    }

    public void MoveBackToNormal()
    {
        StartCoroutine(RotateTo(normalRotation));

    }

    private IEnumerator RotateTo(Quaternion target)
    {
        Quaternion from = transform.rotation;

        //You need to record the rotation at the time you initiated the rotation, and modify the third argument so it goes 0-1.
        //If you don't, the speed of the rotation will slow down the closer it gets to the destination rotation
        for (float t = 0f; t < 1f; t += moveSpeed * Time.deltaTime)
        {
            transform.rotation = Quaternion.Lerp(from, target, t);
            yield return null;
        }

        transform.rotation = target;
    }
}
