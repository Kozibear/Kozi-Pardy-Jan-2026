using System.Collections.Generic;
using UnityEngine;

public class GlobalColorManager : MonoBehaviour
{
    enum GlobalColorState { Blue, Orange, Purple };

    [Header("Global Color State")]
    [SerializeField] GlobalColorState globalColorState = GlobalColorState.Blue;

    [Header("Board Buttons")]
    [SerializeField] List<BoardClueColorChange> boardClueColorChanges;

    [Header("Category Buttons")]
    [SerializeField] List<BoardClueColorChange> CategoryColorChanges;

    [Header("The Board")]
    [SerializeField] BoardSpriteColorChange boardSpriteColorChange;

    [Header("The Wheel")]
    [SerializeField] WheelColorChange wheelColorChange;

    [Header("Background")]
    [SerializeField] BackgroundColorChange backgroundColorChange;

    public void ChangeGlobalColor()
    {
        if (globalColorState == GlobalColorState.Blue)
        {

            globalColorState = GlobalColorState.Orange;
        }
        else if (globalColorState == GlobalColorState.Orange)
        {

            globalColorState = GlobalColorState.Purple;
        }
        else if (globalColorState == GlobalColorState.Purple)
        {

            globalColorState = GlobalColorState.Blue;
        }
    }

    public void DarkenAllOtherBoardButtons(int buttonToWhiteList)
    {
        for (int i = 0; i < boardClueColorChanges.Count; i++)
        {
            if (i  == buttonToWhiteList) continue;
            boardClueColorChanges[i].InstantColorDarken();
        }

        foreach (BoardClueColorChange categoryColorChange in CategoryColorChanges)
        {
            categoryColorChange.InstantColorDarken();
        }
    }
}
