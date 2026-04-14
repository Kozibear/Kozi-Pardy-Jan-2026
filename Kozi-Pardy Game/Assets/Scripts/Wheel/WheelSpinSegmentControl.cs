using KoziPardy.Core;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinSegmentControl : MonoBehaviour
{
    [Header("Wheel Segments")]
    [SerializeField] List<WheelSegment> wheelSegments;
    private List<int> wheelSegmentsAvailable = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    [Header("Board Buttons")]
    [SerializeField] List<BoardClueStateControl> boardClueStateControls;
    private List<int> boardButtonsAvailable = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };

    public List<BoardClueStateControl> GetboardClueStateControls()
    {
        return boardClueStateControls;
    }

    public void UpdatePossibleWheelSegments()
    {
        foreach (BoardClueStateControl boardClueStateControl in boardClueStateControls)
        {
            if (boardClueStateControl != null && boardClueStateControl.GetHasBeenClicked())
            {
                boardButtonsAvailable.Remove(boardClueStateControl.GetNumber());
            }
        }

        if (ButtonsRemovedCheck(new List<int>() { 0, 5, 10, 15, 20 }))
        {
            wheelSegmentsAvailable.Remove(0); //Removes 10s Only
        }

        if (ButtonsRemovedCheck(new List<int>() { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }))
        {
            wheelSegmentsAvailable.Remove(1); //Removes PS4 & PS5
        }

        if (ButtonsRemovedCheck(new List<int>() { 1, 6, 11, 16, 21 }))
        {
            wheelSegmentsAvailable.Remove(2); //Removes 20s Only
        }

        if (ButtonsRemovedCheck(new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }))
        {
            wheelSegmentsAvailable.Remove(3); //Removes PS1 & PS2
        }

        if (ButtonsRemovedCheck(new List<int>() { 2, 7, 12, 17, 22 }))
        {
            wheelSegmentsAvailable.Remove(4); //Removes 30s Only
        }

        if (ButtonsRemovedCheck(new List<int>() { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 }))
        {
            wheelSegmentsAvailable.Remove(5); //Removes PS3 & PS4
        }

        if (ButtonsRemovedCheck(new List<int>() { 3, 8, 13, 18, 23 }))
        {
            wheelSegmentsAvailable.Remove(6); //Removes 40s Only
        }

        if (ButtonsRemovedCheck(new List<int>() { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }))
        {
            wheelSegmentsAvailable.Remove(7); //Removes PS2 & PS3
        }

        if (ButtonsRemovedCheck(new List<int>() { 4, 9, 14, 19, 24 }))
        {
            wheelSegmentsAvailable.Remove(8); //Removes 50s Only
        }

        if (ButtonsRemovedCheck(new List<int>() { 0, 1, 2, 3, 4, 20, 21, 22, 23, 24 }))
        {
            wheelSegmentsAvailable.Remove(9); //Removes PS1 & PS5
        }

        AssignWheelSegments();
    }

    private bool ButtonsRemovedCheck(List <int> checkList)
    {
        int buttonsRemoved = 0;

        foreach (int intcheck in checkList)
        {
            if (!boardButtonsAvailable.Contains(intcheck)) buttonsRemoved++;
        }

        if (buttonsRemoved == checkList.Count) return true;
        else return false;
    }

    void AssignWheelSegments()
    {
        List<int> thisTurnsWheelSegments = new List<int>();

        while (thisTurnsWheelSegments.Count < 10)
        {
            for (int i = 0; i < wheelSegmentsAvailable.Count; i++)
            {
                thisTurnsWheelSegments.Add(wheelSegmentsAvailable[i]);

                if (thisTurnsWheelSegments.Count == 10) break;

                //If we're at exactly 9 thisTurnsWheelSegments, and we currently have 9 wheelSegmentsAvailable (down one)
                //the 10th thisTurnsWheelSegments is automatically set to be the 6th wheelSegmentAvailable
                if (thisTurnsWheelSegments.Count == 9 && wheelSegmentsAvailable.Count == 9) 
                {
                    thisTurnsWheelSegments.Add(wheelSegmentsAvailable[5]);
                    break;
                }
            }
        }

        for (int i = 0; i < wheelSegments.Count; i++)
        {
            wheelSegments[i].SetCurrentSegment(thisTurnsWheelSegments[i]);
            wheelSegments[i].DisplaySegmentName();
        }
    }

    public void DestroyWheelSegments()
    {
        foreach (WheelSegment segment in wheelSegments) { segment.DestroyChild(); }
    }
}