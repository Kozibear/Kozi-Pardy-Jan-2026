using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinBoardSelections : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameManager gameManager;

    [Header("White Flash Sprite Fade")]
    [SerializeField] SpriteFade whiteFlash;

    [Header("Wait Times")]
    [SerializeField] float waitBeforeFlash;
    [SerializeField] float waitAfterFlash;

    List<BoardClueStateControl> boardClueStateControls;

    private int currentSelectedSegment;
    private List<int> currentList;

    private List<int> tensOnlyList = new List<int>() { 0, 5, 10, 15, 20 };
    private List<int> columnsFourFive = new List<int>() { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };

    private List<int> twentiesOnlyList = new List<int>() { 1, 6, 11, 16, 21 };
    private List<int> columnsOneTwo = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    private List<int> thirtiesOnlyList = new List<int>() { 2, 7, 12, 17, 22 };
    private List<int> columnsThreeFour = new List<int>() { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };

    private List<int> fortiesOnlyList = new List<int>() { 3, 8, 13, 18, 23 };
    private List<int> columnsTwoThree = new List<int>() { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

    private List<int> fiftiesOnlyList = new List<int>() { 4, 9, 14, 19, 24 };
    private List<int> columnsOneFive = new List<int>() { 0, 1, 2, 3, 4, 20, 21, 22, 23, 24 };

    private List<int> wholeBoardList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };

    private void Start()
    {
        boardClueStateControls = GetComponent<WheelSpinSegmentControl>().GetboardClueStateControls();
    }

    public void LightUpBoardParts(int selectedSegment)
    {
        currentSelectedSegment = selectedSegment;

        foreach (BoardClueStateControl clue in boardClueStateControls)
        {
            clue.InstantDarken();
        }

        switch (currentSelectedSegment)
        {
            case 0:
                foreach (int i in  tensOnlyList) { boardClueStateControls[i].InstantLighten(); }
                currentList = tensOnlyList;
                break;

            case 1:
                foreach (int i in columnsFourFive) { boardClueStateControls[i].InstantLighten(); }
                currentList = columnsFourFive;
                break;

            case 2:
                foreach (int i in twentiesOnlyList) { boardClueStateControls[i].InstantLighten(); }
                currentList = twentiesOnlyList;
                break;

            case 3:
                foreach (int i in columnsOneTwo) { boardClueStateControls[i].InstantLighten(); }
                currentList = columnsOneTwo;
                break;

            case 4:
                foreach (int i in thirtiesOnlyList) { boardClueStateControls[i].InstantLighten(); }
                currentList = thirtiesOnlyList;
                break;

            case 5:
                foreach (int i in columnsThreeFour) { boardClueStateControls[i].InstantLighten(); }
                currentList = columnsThreeFour;
                break;

            case 6:
                foreach (int i in fortiesOnlyList) { boardClueStateControls[i].InstantLighten(); }
                currentList = fortiesOnlyList;
                break;

            case 7:
                foreach (int i in columnsTwoThree) { boardClueStateControls[i].InstantLighten(); }
                currentList = columnsTwoThree;
                break;

            case 8:
                foreach (int i in fiftiesOnlyList) { boardClueStateControls[i].InstantLighten(); }
                currentList = fiftiesOnlyList;
                break;

            case 9:
                foreach (int i in columnsOneFive) { boardClueStateControls[i].InstantLighten(); }
                currentList = columnsOneFive;
                break;

            default:
                foreach (int i in wholeBoardList) { boardClueStateControls[i].InstantLighten(); }
                currentList = wholeBoardList;
                break;
        }
    }

    public void ConfirmBoardSelection()
    {
        StartCoroutine(TellGameManagerButtonsToActivate());
    }

    IEnumerator TellGameManagerButtonsToActivate()
    {
        yield return new WaitForSeconds(waitBeforeFlash);
        whiteFlash.Pulse(1);

        yield return new WaitForSeconds(waitAfterFlash);

        gameManager.HideWheelAndReturnToBoard(currentList);
    }
}
