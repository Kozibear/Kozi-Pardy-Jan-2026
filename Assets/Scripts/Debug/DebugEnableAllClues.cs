using System.Collections.Generic;

using UnityEngine;

public class DebugEnableAllClues : MonoBehaviour
{
    [SerializeField] GameBoardManager gameBoardManager;
    [SerializeField] ButtonCanvasControl buttonCanvasControl;
    [SerializeField] GameObject boardButtons;
    [SerializeField] DebugOptionsShow debugOptionsShow;

    private List<int> allButtonNumbers = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };

    public void EnableAllClues()
    {
        if (gameBoardManager.GetWheelIsActive()) return;

        foreach (Transform child in boardButtons.transform)
        {
            child.GetComponent<BoardClueStateControl>().InstantLighten();
        }

        buttonCanvasControl.ActivateSpecificNewCluesAndOldClues(allButtonNumbers);

        debugOptionsShow.ToggleButtons();
    }
}
