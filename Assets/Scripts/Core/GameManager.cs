using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState { Singleboard, DoubleBoard, FinalBoard };

    [Header("Wheel Spin")]
    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] WheelSpinMovement wheelSpinMovement;
    [SerializeField] float waitBeforeBringingInWheel = 1f;

    [Header("Button Canvas Control")]
    [SerializeField] ButtonCanvasControl buttonCanvasControl;

    [Header("Black Background Fade")]
    [SerializeField] SpriteFade blackBackgroundFade;

    [Header("Clues Left")]
    [SerializeField] int cluesLeft = 25;

    private List<int> storedButtonsToActivate;
    private bool automaticWheelspins = false;
    private bool wheelIsActive = false;

    //[Header("Game State Enum")]
    //[SerializeField] GameState gameState = GameState.Singleboard;

    public void BoardBeforeWheelSpin()
    {
        cluesLeft--;

        if (cluesLeft > 1)
        {
            if (automaticWheelspins) StartCoroutine(WheelSpinSetup());
            else buttonCanvasControl.ActivateWheelSpinButton();
        }
        else if (cluesLeft == 1)
        {
            buttonCanvasControl.FinalButtonActivation();
        }
        else if (cluesLeft <= 0)
        {
            EndState();
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

        wheelIsActive = true;

        cameraMovement.MoveForWheelSpin();
        wheelSpinMovement.StartWheelSpin();

        blackBackgroundFade.SetFadeInThreshold(0.4f);
        blackBackgroundFade.SetFadeSpeeds(2.8f);
        blackBackgroundFade.FadeIn();
    }

    public void HideWheelAndReturnToBoard(List<int> buttonsToActivate)
    {
        storedButtonsToActivate = buttonsToActivate;

        cameraMovement.MoveBackToNormal();
        wheelSpinMovement.MoveOutWheelSpin();
        blackBackgroundFade.FadeOut();
    }

    public void ActivateBoardClues()
    {
        wheelIsActive = false;

        buttonCanvasControl.ActivateSpecificNewCluesAndOldClues(storedButtonsToActivate);
    }

    public void ToggleAutoWheelSpins(bool value)
    {
        automaticWheelspins = value;
    }

    void EndState()
    {
        buttonCanvasControl.ResetEverything();

    }

    public bool GetWheelIsAuto() { return automaticWheelspins; }

    public bool GetWheelIsActive() { return wheelIsActive; }

    public int GetCluesLeft() { return cluesLeft; }
}