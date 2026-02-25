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

    public void LightUpBoardParts(int selectedSegment)
    {
        currentSelectedSegment = selectedSegment;
        boardClueStateControls = GetComponent<WheelSpinSegmentControl>().GetboardClueStateControls();

        foreach (BoardClueStateControl clue in boardClueStateControls)
        {
            clue.InstantDarken();
        }

        switch (currentSelectedSegment)
        {
            case 0:
                for (int i = 0; i < tensOnlyList.Count; i++)
                {
                    boardClueStateControls[tensOnlyList[i]].InstantLighten();
                }
                break;

            case 1:
                for (int i = 0; i < columnsFourFive.Count; i++)
                {
                    boardClueStateControls[columnsFourFive[i]].InstantLighten();
                }
                break;

            case 2:
                for (int i = 0; i < twentiesOnlyList.Count; i++)
                {
                    boardClueStateControls[twentiesOnlyList[i]].InstantLighten();
                }
                break;

            case 3:
                for (int i = 0; i < columnsOneTwo.Count; i++)
                {
                    boardClueStateControls[columnsOneTwo[i]].InstantLighten();
                }
                break;

            case 4:
                for (int i = 0; i < thirtiesOnlyList.Count; i++)
                {
                    boardClueStateControls[thirtiesOnlyList[i]].InstantLighten();
                }
                break;

            case 5:
                for (int i = 0; i < columnsThreeFour.Count; i++)
                {
                    boardClueStateControls[columnsThreeFour[i]].InstantLighten();
                }
                break;

            case 6:
                for (int i = 0; i < fortiesOnlyList.Count; i++)
                {
                    boardClueStateControls[fortiesOnlyList[i]].InstantLighten();
                }
                break;

            case 7:
                for (int i = 0; i < columnsTwoThree.Count; i++)
                {
                    boardClueStateControls[columnsTwoThree[i]].InstantLighten();
                }
                break;

            case 8:
                for (int i = 0; i < fiftiesOnlyList.Count; i++)
                {
                    boardClueStateControls[fiftiesOnlyList[i]].InstantLighten();
                }
                break;

            case 9:
                for (int i = 0; i < columnsOneFive.Count; i++)
                {
                    boardClueStateControls[columnsOneFive[i]].InstantLighten();
                }
                break;

            default:
                for (int i = 0; i < wholeBoardList.Count; i++)
                {
                    boardClueStateControls[wholeBoardList[i]].InstantLighten();
                }
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

        switch (currentSelectedSegment)
        {
            case 0:
                gameManager.HideWheelAndReturnToBoard(tensOnlyList);
                break;

            case 1:
                gameManager.HideWheelAndReturnToBoard(columnsFourFive);
                break;

            case 2:
                gameManager.HideWheelAndReturnToBoard(twentiesOnlyList);
                break;

            case 3:
                gameManager.HideWheelAndReturnToBoard(columnsOneTwo);
                break;

            case 4:
                gameManager.HideWheelAndReturnToBoard(thirtiesOnlyList);
                break;

            case 5:
                gameManager.HideWheelAndReturnToBoard(columnsThreeFour);
                break;

            case 6:
                gameManager.HideWheelAndReturnToBoard(fortiesOnlyList);
                break;

            case 7:
                gameManager.HideWheelAndReturnToBoard(columnsTwoThree);
                break;

            case 8:
                gameManager.HideWheelAndReturnToBoard(fiftiesOnlyList);
                break;

            case 9:
                gameManager.HideWheelAndReturnToBoard(columnsOneFive);
                break;

            default:
                gameManager.HideWheelAndReturnToBoard(wholeBoardList);
                break;
        }
    }
}
