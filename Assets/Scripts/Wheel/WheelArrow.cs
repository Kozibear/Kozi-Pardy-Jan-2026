using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelArrow : MonoBehaviour
{

    [SerializeField] GameManager gameManager;

    [SerializeField] float wait;

    private int currentSelectedSegment;

    private List<int> onesOnlyList = new List<int>() { 0, 5, 10, 15, 20, 25 };
    private List<int> twosOnlyList = new List<int>() { 1, 6, 11, 16, 21, 26 };
    private List<int> threesOnlyList = new List<int>() { 2, 7, 12, 17, 22, 27 };
    private List<int> foursOnlyList = new List<int>() { 3, 8, 13, 18, 23, 28 };
    private List<int> fivesOnlyList = new List<int>() { 4, 9, 14, 19, 24, 29 };
    private List<int> leftThirdList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private List<int> middleThirdList = new List<int>() { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
    private List<int> rightThirdList = new List<int>() { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };
    private List<int> leftHalfList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
    private List<int> rightHalfList = new List<int>() { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };
    private List<int> wholeBoardList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentSelectedSegment = collision.GetComponent<WheelSegment>().GetCurrentSegment();
    }

    public void SelectSegment()
    {
        StartCoroutine(TellGameManagerButtonsToActivate());
    }

    IEnumerator TellGameManagerButtonsToActivate()
    {
        yield return new WaitForSeconds(wait);

        switch (currentSelectedSegment)
        {
            case 0:
                gameManager.ActivateButtonsAndHideWheel(onesOnlyList);
                break;

            case 1:
                gameManager.ActivateButtonsAndHideWheel(leftThirdList);
                break;

            case 2:
                gameManager.ActivateButtonsAndHideWheel(twosOnlyList);
                break;

            case 3:
                gameManager.ActivateButtonsAndHideWheel(middleThirdList);
                break;

            case 4:
                gameManager.ActivateButtonsAndHideWheel(threesOnlyList);
                break;

            case 5:
                gameManager.ActivateButtonsAndHideWheel(rightThirdList);
                break;

            case 6:
                gameManager.ActivateButtonsAndHideWheel(foursOnlyList);
                break;

            case 7:
                gameManager.ActivateButtonsAndHideWheel(leftHalfList);
                break;

            case 8:
                gameManager.ActivateButtonsAndHideWheel(fivesOnlyList);
                break;

            case 9:
                gameManager.ActivateButtonsAndHideWheel(rightHalfList);
                break;

            default:
                gameManager.ActivateButtonsAndHideWheel(wholeBoardList);
                break;
        }
    }
}
