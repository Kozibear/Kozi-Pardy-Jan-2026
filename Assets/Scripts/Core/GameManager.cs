using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState { Singleboard, DoubleBoard, FinalBoard };

    [Header("Wheel Spin")]
    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] WheelSpinMovement wheelSpinMovement;
    [SerializeField] float waitBeforeBringingInWheel = 1f;

    [Header("Board Buttons")]
    [SerializeField] List<GameObject> boardButtons;

    [Header("ClueScreen")]
    [SerializeField] ClueScreen clueScreen;
    private bool canShowClue = true;

    [Header("Clues")]
    ClueSO currentClue;

    [Header("Black Background Fade")]
    [SerializeField] SpriteFade blackBackgroundFade;

    //[Header("Game State Enum")]
    //[SerializeField] GameState gameState = GameState.Singleboard;

    public void BoardBeforeWheelSpin()
    {
        StartCoroutine(WheelSpinSetup());
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

    public void ActivateButtonsAndHideWheel(List<int> buttonsToActivate)
    {
        foreach (int buttonToActivate in buttonsToActivate)
        {
            //boardButtons[buttonToActivate].GetComponent<BoardButton>().InteractableFadeIn();
        }

        cameraMovement.MoveBackToNormal();
        wheelSpinMovement.MoveOutWheelSpin();
        blackBackgroundFade.FadeOut();
    }

    public void OnClueSelected() { }

    public void HardDeActivationAndHidePointNumber() { }

    public void BackButtonPressed() { }
}