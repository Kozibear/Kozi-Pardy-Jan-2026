using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoziPardy.Core
{
    public class GameBoardManager : MonoBehaviour
    {
        [Header("Wheel Spin")]
        [SerializeField] List<CameraMovement> cameraMovements;
        [SerializeField] WheelSpinMovement wheelSpinMovement;
        [SerializeField] float waitBeforeBringingInWheel = 1f;

        [Header("UI Buttons")]
        [SerializeField] ButtonCanvasControl buttonCanvasControl;
        [SerializeField] DebugOptionsShow showDebugOptionsButton;

        [Header("Black Background Fade")]
        [SerializeField] SpriteFade blackBackgroundFade;

        [Header("Clues Left")]
        [SerializeField] int cluesLeft = 25;

        private List<int> storedButtonsToActivate;
        private bool automaticWheelspins = false;
        private bool wheelIsActive = false;

        public void BoardBeforeNextTurn()
        {
            cluesLeft--;

            if (cluesLeft > 1)
            {
                if (automaticWheelspins && GameSettings.wheelSpinGame)
                {
                    StartCoroutine(WheelSpinSetup());
                }
                else
                {
                    buttonCanvasControl.NewTurnButtonActivations();
                }
            }
            else if (cluesLeft <= 1)
            {
                buttonCanvasControl.FinalButtonActivation();
            }
        }

        private IEnumerator WheelSpinSetup()
        {
            yield return new WaitForSeconds(waitBeforeBringingInWheel);
            ShowWheel();
        }

        public void ShowWheel()
        {
            buttonCanvasControl.ResetEverything();
            buttonCanvasControl.DisableDebug();

            wheelIsActive = true;

            foreach (CameraMovement camera in cameraMovements) { camera.MoveForWheelSpin(); }
            wheelSpinMovement.StartWheelSpin();
        }

        public void HideWheelAndReturnToBoard(List<int> buttonsToActivate)
        {
            storedButtonsToActivate = buttonsToActivate;

            foreach (CameraMovement camera in cameraMovements) { camera.MoveBackToNormal(); }
            wheelSpinMovement.MoveOutWheelSpin();
        }

        public void ActivateSpecificBoardClues()
        {
            wheelIsActive = false;

            buttonCanvasControl.ActivateSpecificNewCluesAndOldClues(storedButtonsToActivate);
        }

        public void ToggleAutoWheelSpins(bool value)
        {
            automaticWheelspins = value;
        }

        public bool GetWheelIsAuto() { return automaticWheelspins; }

        public bool GetWheelIsActive() { return wheelIsActive; }

        public int GetCluesLeft() { return cluesLeft; }
    }
}