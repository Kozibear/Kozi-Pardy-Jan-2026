using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using KoziPardy.ColorManagement;

public class ButtonCanvasControl : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] List<Button> boardButtons;
    [SerializeField] Button backButton;
    [SerializeField] Button wheelSpinButton;
    [SerializeField] GameObject gameManagerObject;

    [Header("Board Clues")]
    [SerializeField] List<BoardClueStateControl> boardClueStateControls;

    private List<int> currentActivatedButtons;
    private bool oldClueIsBeingShown = false;
    private bool newClueHasBeenShownThisTurn = false;
    private bool readyForNextTurn = false;

    public void ActivateSpecificNewCluesAndOldClues(List<int> buttonsToActivate)
    {
        currentActivatedButtons = buttonsToActivate;

        foreach (int i in buttonsToActivate)
        {
            if (!boardClueStateControls[i].GetHasBeenClicked())
            {
                boardButtons[i].gameObject.SetActive(true);
            }
        }

        ActivateJustOldClues();
    }

    void ActivateJustOldClues()
    {
        foreach (BoardClueStateControl bCSC in boardClueStateControls)
        {
            if (bCSC.GetHasBeenClicked())
            {
                boardButtons[bCSC.GetNumber()].gameObject.SetActive(true);
            }
        }
    }
    
    public void ClueHasBeenSelected()
    {
        setAllBoardButtonsState(false);
        wheelSpinButton.gameObject.SetActive(false);
    }

    public void ClueIsUpFront(int buttonToWhiteList, bool hasClueBeenClicked)
    {
        backButton.gameObject.SetActive(true);

        if (hasClueBeenClicked)
        {
            oldClueIsBeingShown = true;
        }
        else
        {
            newClueHasBeenShownThisTurn = true;
            gameManagerObject.GetComponent<GlobalColorManager>().DarkenAllOtherBoardButtons(buttonToWhiteList);
        }
    }

    public void BackButtonBeenSelected()
    {
        if (!oldClueIsBeingShown)
        {
            gameManagerObject.GetComponent<GlobalColorManager>().ChangeGlobalColor();
        }
    }

    public void ClueIsBackHome()
    {
        if (oldClueIsBeingShown)
        {
            if (newClueHasBeenShownThisTurn)
            {
                ActivateJustOldClues();
            }
            else
            {
                ActivateSpecificNewCluesAndOldClues(currentActivatedButtons);
            }

            oldClueIsBeingShown = false;
        }
        else //i.e. if a new clue was just being shown
        {
            //we don't want any issues from calling an old clue onscreen while the wheel is in auto mode
            //so if the wheel is in auto mode, we aren't allowed to activate old clues, or activate the wheelspin button
            if (!gameManagerObject.GetComponent<GameManager>().GetWheelIsAuto())
            {
                readyForNextTurn = true;
                ActivateJustOldClues();
            }

            gameManagerObject.GetComponent<GameManager>().BoardBeforeWheelSpin();
        }

        if (readyForNextTurn)
        {
            ActivateWheelSpinButton();
        }
    }

    public void ActivateWheelSpinButton()
    {
        wheelSpinButton.gameObject.SetActive(true);
    }

    public void ResetEverything()
    {
        oldClueIsBeingShown = false;
        newClueHasBeenShownThisTurn = false;
        readyForNextTurn = false;

        setAllBoardButtonsState(false);
        backButton.gameObject.SetActive(false);
        wheelSpinButton.gameObject.SetActive(false);
    }

    public void setAllBoardButtonsState(bool state)
    {
        foreach (Button boardButton in boardButtons)
        {
            if (boardButton != null) boardButton.gameObject.SetActive(state);
        }
    }
}
