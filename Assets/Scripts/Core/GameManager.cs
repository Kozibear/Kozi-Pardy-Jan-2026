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

    private List<int> storedButtonsToActivate;

    [Header("Clues Left")]
    [SerializeField] float cluesLeft = 25;

    //[Header("Game State Enum")]
    //[SerializeField] GameState gameState = GameState.Singleboard;

    public void BoardBeforeWheelSpin()
    {
        cluesLeft--;
        if (cluesLeft > 0)
        {
            StartCoroutine(WheelSpinSetup());
        }
    }

    private IEnumerator WheelSpinSetup()
    {
        yield return new WaitForSeconds(waitBeforeBringingInWheel);
        ShowWheel();
    }

    public void ShowWheel()
    {
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
        buttonCanvasControl.ActivateSpecificClues(storedButtonsToActivate);
    }

    public void HardDeActivationAndHidePointNumber() { }
}