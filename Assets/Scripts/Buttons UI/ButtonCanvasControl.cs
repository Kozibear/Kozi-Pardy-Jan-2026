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
    GameManager gameManager;
    GlobalColorManager globalColorManager;

    [Header("Board Clues")]
    [SerializeField] List<BoardClueStateControl> boardClueStateControls;

    [Header("Vignette Fader")]
    [SerializeField] VignetteFader vignetteFader;


    private List<int> currentActivatedButtons;
    private bool oldClueIsBeingShown = false;
    private bool newClueHasBeenShownThisTurn = false;
    private bool readyForNextTurn = false;
    private bool finalClue = false;

    private void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        globalColorManager = gameManagerObject.GetComponent<GlobalColorManager>();
    }

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

    public void FinalButtonActivation()
    {
        ResetEverything();

        foreach (BoardClueStateControl clueStateControl in boardClueStateControls)
        {
            if (!clueStateControl.GetHasBeenClicked())
            {
                boardButtons[clueStateControl.GetNumber()].gameObject.SetActive(true);
                clueStateControl.GradualBrightenIfFinal();
            }
        }

        finalClue = true;
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

        vignetteFader.StartVigentteFadeIn();
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
            globalColorManager.DarkenAllOtherBoardButtons(buttonToWhiteList);
        }
    }

    public void BackButtonBeenSelected()
    {
        if (!oldClueIsBeingShown && !finalClue)
        {
            globalColorManager.ChangeGlobalColor();
        }

        vignetteFader.StartVigentteFadeOut();
    }

    public void ClueIsBackHome()
    {
        if (oldClueIsBeingShown)
        {
            if (newClueHasBeenShownThisTurn)
            {
                ActivateJustOldClues();
            }
            else //we need to make sure the new clue(s) are reactivated
            {
                if (finalClue)
                {
                    FinalButtonActivation();
                }
                else
                {
                    ActivateSpecificNewCluesAndOldClues(currentActivatedButtons);
                }
            }

            oldClueIsBeingShown = false;
        }

        else //i.e. if a new clue was just shown
        {
            //we don't want any issues from calling an old clue onscreen while the wheel is in auto mode, OR when we have 1 or fewer clues left
            //So in either case, we aren't allowed to activate old clues, or activate the wheelspin button
            if (!gameManager.GetWheelIsAuto() || gameManager.GetCluesLeft() <= 2)
            {
                readyForNextTurn = true;
                ActivateJustOldClues();
            }

            gameManager.BoardBeforeWheelSpin();
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
