using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BoardClueMovement : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] Vector3 frontPosition;
    [SerializeField] Vector3 originalPosition;

    [Header("Rotations")]
    [SerializeField] Quaternion frontRotation;
    [SerializeField] Quaternion originalRotation;

    [Header("Speeds")]
    [SerializeField] float positionMoveSpeed = 1;

    [Header("Wait Times")]
    [SerializeField] float waitBeforeRotateIn = 0.05f;
    [SerializeField] float waitBeforeRotateOut = 0;

    [Header("ButtonCanvasControl")]
    [SerializeField] ButtonCanvasControl buttonCanvasControl;

    //[Header("Audio Source")]
    //[SerializeField] AudioSource audioSource;
    //[SerializeField] float waitBeforePlayingAudio = 1;
    //[SerializeField] float waitBeforeStoppingAudio = 1;

    private Vector3 destinationPosition;
    private bool canMoveClue = false;
    private bool isUpFront = false;

    void Start()
    {
        originalPosition = transform.position;
        destinationPosition = transform.position;

        originalRotation = transform.rotation;
    }

    void Update()
    {
        float step = Time.deltaTime * positionMoveSpeed;

        if (canMoveClue)
        {
            if (transform.localPosition != destinationPosition) MoveToDestination(step);
            else ArrivedAtDestination();
        }
    }

    private void MoveToDestination(float step)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destinationPosition, step);
    }

    private IEnumerator RotateToDestination(Quaternion target, float wait)
    {
        yield return new WaitForSeconds(wait);
        Quaternion from = transform.rotation;

        float startDistance = Vector3.Distance(transform.localPosition, destinationPosition);

        while (transform.localPosition != destinationPosition)
        { 
            float currentDistance = Vector3.Distance(transform.localPosition, destinationPosition);
            float distanceDifference = (startDistance - currentDistance) / startDistance;

            transform.rotation = Quaternion.Slerp(from, target, distanceDifference);

            yield return null;
        }
        transform.rotation = target;
    }

    private void ArrivedAtDestination()
    {
        canMoveClue = false;

        if (transform.localPosition == frontPosition)
        {
            isUpFront = true;
            GetComponent<BoardClueMediaManager>().HideFrontText();

            buttonCanvasControl.ClueIsUpFront(GetComponent<BoardClueStateControl>().GetNumber(), GetComponent<BoardClueStateControl>().GetHasBeenClicked());

            GetComponent<BoardClueStateControl>().SetHasBeenClicked(true); //this must come after the line above, as otherwise it'll get marked as an old question even if it's not
        }
        if (transform.localPosition == originalPosition)
        {
            isUpFront = false;
            GetComponent<BoardClueMediaManager>().ClueCleanup();

            buttonCanvasControl.ClueIsBackHome();
        }
    }

    public void MoveInClue()
    {
        if (!isUpFront)
        {
            destinationPosition = frontPosition;
            canMoveClue = true;

            StartCoroutine(RotateToDestination(frontRotation, waitBeforeRotateIn));

            GetComponent<BoardClueMediaManager>().CluePrep();
            GetComponent<BoardClueStateControl>().GradualBrightenIfOld();
        }
    }

    public void MoveOutClue()
    {
        if (isUpFront)
        {
            destinationPosition = originalPosition;
            canMoveClue = true;

            StartCoroutine(RotateToDestination(originalRotation, waitBeforeRotateOut));

            GetComponent<BoardClueStateControl>().GradualDarkenIfOld();
        }
    }

    //private IEnumerator AudioSwoosh()
    //{
    //    yield return new WaitForSeconds(waitBeforePlayingAudio);
    //    audioSource.Play();
    //    yield return new WaitForSeconds(waitBeforeStoppingAudio);
    //    audioSource.Stop();
    //}
}
