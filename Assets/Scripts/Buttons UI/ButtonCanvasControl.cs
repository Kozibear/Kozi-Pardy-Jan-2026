using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using KoziPardy.ColorManagement;

namespace KoziPardy.Core
{
    public class ButtonCanvasControl : MonoBehaviour
    {
        [Header("UI Buttons")]
        [SerializeField] List<Button> boardButtons;
        [SerializeField] Button backButton;
        [SerializeField] Button wheelSpinButton;
        [SerializeField] Button rightSFXButton;
        [SerializeField] Button wrongSFXButton;

        [Header("Debug Buttons")]
        [SerializeField] DebugOptionsShow showDebugOptionsButton;

        [Header("Game Manager")]
        [SerializeField] GameObject gameManagerObject;

        [Header("Board Clues")]
        [SerializeField] List<BoardClueStateControl> boardClueStateControls;

        [Header("Vignette Fader")]
        [SerializeField] VignetteFader vignetteFader;

        GameBoardManager gameBoardManager;
        GlobalColorManager globalColorManager;

        private List<int> currentActivatedButtons;
        private bool oldClueIsBeingShown = false;
        private bool newClueHasBeenShownThisTurn = false;
        private bool readyForNextWheelSpin = false;
        private bool finalClue = false;

        private void Start()
        {
            gameBoardManager = gameManagerObject.GetComponent<GameBoardManager>();
            globalColorManager = gameManagerObject.GetComponent<GlobalColorManager>();

            showDebugOptionsButton.gameObject.SetActive(true);

            rightSFXButton.gameObject.SetActive(false);
            wrongSFXButton.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);

            if (GameSettings.WheelSpinGame) SetAllBoardButtonsState(false);

            NewTurnButtonActivations();
        }

        public void ActivateSpecificNewCluesAndOldClues(List<int> buttonsToActivate)
        {
            showDebugOptionsButton.gameObject.SetActive(true);

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
            showDebugOptionsButton.gameObject.SetActive(true);

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
            SetAllBoardButtonsState(false);
            wheelSpinButton.gameObject.SetActive(false);
            DisableDebug();

            vignetteFader.StartVigentteFadeIn();
        }

        public void ClueIsUpFront(int buttonToWhiteList, bool hasClueBeenClicked)
        {
            ClueUpFrontButtons(true);

            if (hasClueBeenClicked)
            {
                oldClueIsBeingShown = true;
            }
            else
            {
                newClueHasBeenShownThisTurn = true;
                if (GameSettings.WheelSpinGame) globalColorManager.DarkenAllOtherBoardButtons(buttonToWhiteList);
            }
        }

        public void BackButtonBeenSelected()
        {
            ClueUpFrontButtons(false);
            showDebugOptionsButton.gameObject.SetActive(true);

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
                //If we're a wheelspin game, we don't want any issues from calling an old clue onscreen while the wheel is in auto mode, OR when we have 1 or fewer clues left
                //So in either case, we aren't allowed to activate old clues, or activate the wheelspin button
                if (GameSettings.WheelSpinGame && (!gameBoardManager.GetWheelIsAuto() || gameBoardManager.GetCluesLeft() <= 2))
                {
                    readyForNextWheelSpin = true;
                    ActivateJustOldClues();
                }

                gameBoardManager.BoardBeforeNextTurn();
            }

            if (!GameSettings.WheelSpinGame || readyForNextWheelSpin)
            {
                NewTurnButtonActivations();
            }
        }

        public void NewTurnButtonActivations()
        {
            if (GameSettings.WheelSpinGame)
            {
                wheelSpinButton.gameObject.SetActive(true);
            }
            else
            {
                wheelSpinButton.gameObject.SetActive(false);
                SetAllBoardButtonsState(true);
            }

        }

        public void ResetEverything()
        {
            oldClueIsBeingShown = false;
            newClueHasBeenShownThisTurn = false;
            readyForNextWheelSpin = false;

            SetAllBoardButtonsState(false);
            backButton.gameObject.SetActive(false);
            wheelSpinButton.gameObject.SetActive(false);
        }

        public void SetAllBoardButtonsState(bool state)
        {
            foreach (Button boardButton in boardButtons)
            {
                if (boardButton != null) boardButton.gameObject.SetActive(state);
            }
        }

        public void DisableDebug()
        {
            showDebugOptionsButton.HideButtons();
            showDebugOptionsButton.gameObject.SetActive(false);
        }

        private void ClueUpFrontButtons(bool state)
        {
            backButton.gameObject.SetActive(state);
            rightSFXButton.gameObject.SetActive(state);
            wrongSFXButton.gameObject.SetActive(state);
        }
    }
}