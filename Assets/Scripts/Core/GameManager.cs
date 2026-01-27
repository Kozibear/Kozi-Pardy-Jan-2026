using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState { Singleboard, DoubleBoard, FinalBoard };

    [Header("Blur")]
    [SerializeField] Blur blur;

    [Header("Wheel Spin")]
    [SerializeField] WheelSpin wheelSpin;
    [SerializeField] float waitBeforeBringingInWheel = 1f;
    private bool wheelSpinButtonActive = true;

    [Header("Bottom Buttons")]
    [SerializeField] WheelspinButton wheelSpinButton;
    [SerializeField] DoublesBoardButton doublesBoardButton;
    private bool beforeSwitchToDoubles = false;

    [Header("Board Buttons")]
    [SerializeField] List<GameObject> boardButtons;

    [Header("Board Title Changes")]
    [SerializeField] List<CategoryTitleChange> categoryTitleChanges;

    [Header("ClueScreen")]
    [SerializeField] ClueScreen clueScreen;
    private bool canShowClue = true;

    [Header("Clues SOs")]
    [SerializeField] List<ClueSO> clues;
    [SerializeField] List<ClueSO> cluesDouble;
    ClueSO currentClue;

    [Header("Clue Text/Media")]
    [SerializeField] List<TextMeshPro> clueText;

    [SerializeField] List<TextMeshPro> haikuClueText;
    [SerializeField] List<TextMeshPro> haikuClueTextShadows;

    [SerializeField] List<TextMeshPro> songLyricText;
    [SerializeField] List<TextMeshPro> songLyricTextShadows;

    [SerializeField] GameObject clueImageSpawnPoint;

    [Header("Black Background Fade")]
    [SerializeField] SpriteFade blackBackgroundFade;

    [Header("Panic Button")]
    [SerializeField] PanicButton panicButton;

    [Header("Game State Enum")]
    [SerializeField] GameState gameState = GameState.Singleboard;

    [Header("Only Singles Board?")]
    [SerializeField] bool onlySingles;

    private int currentIndex;

    public void BoardBeforeWheelSpin()
    {
        HideClueTexts();
        StartCoroutine(WheelSpinSetup());
    }

    public void HideClueTexts()
    {
        foreach (TextMeshPro standardClue in clueText) standardClue.gameObject.SetActive(false);

        foreach (TextMeshPro haikuClue in haikuClueText) haikuClue.gameObject.SetActive(false);
        foreach (TextMeshPro haikuClueShadow in haikuClueTextShadows) haikuClueShadow.gameObject.SetActive(false);

        foreach (TextMeshPro songLyric in songLyricText) songLyric.gameObject.SetActive(false);
        foreach (TextMeshPro songLyricShadow in songLyricTextShadows) songLyricShadow.gameObject.SetActive(false);

        //destruction of child image prefabs is handled in the ClueScreen script
    }

    private IEnumerator WheelSpinSetup()
    {
        if (!beforeSwitchToDoubles)
        {
            if (wheelSpinButtonActive)
            {
                wheelSpinButton.gameObject.SetActive(true);
                wheelSpinButton.BrightenAndInteractable();
            }
            else
            {
                yield return new WaitForSeconds(waitBeforeBringingInWheel);
                ShowWheel();
            }
        }

        yield return null;
    }
    public void ShowWheel()
    {
        wheelSpinButtonActive = false;
        wheelSpin.StartWheelSpin();

        blackBackgroundFade.SetFadeInThreshold(0.4f);
        blackBackgroundFade.SetFadeSpeeds(2.8f);
        blackBackgroundFade.FadeIn();
        blur.StartBlurBackground();
    }

    public void ActivateButtonsAndHideWheel(List<int> buttonsToActivate)
    {
        foreach (int buttonToActivate in buttonsToActivate)
        {
            boardButtons[buttonToActivate].GetComponent<BoardButton>().InteractableFadeIn();
        }
        wheelSpin.MoveOutWheelSpin();
        blackBackgroundFade.FadeOut();
        blur.StartUnBlurBackground();
        canShowClue = true;
    }

    public void OnClueSelected(int index)
    {
        if (canShowClue)
        {
            panicButton.NotInteractable();
            canShowClue = false;

            //we show the current clue/haiku on the clue screen
            DisplayClue(index);

            //we make all the buttons Non-Interactable, but not dark yet
            foreach (GameObject boardButton in boardButtons) { boardButton.GetComponent<BoardButton>().SoftNotInteractable(); }

            //we move the cluescreen in
            clueScreen.MoveInClue();

            currentIndex = index;
        }
    }

    private void DisplayClue(int index)
    {
        switch (clues[index].GetClueType())
        {
            case 1: //is Haiku
                for (int i = 0; i < haikuClueText.Count; i++)
                {
                    haikuClueText[i].text = clues[index].GetHaikuClueText(i);
                    haikuClueText[i].gameObject.SetActive(true);

                    haikuClueTextShadows[i].text = clues[index].GetHaikuClueText(i);
                    haikuClueTextShadows[i].gameObject.SetActive(true);
                }
                break;

            case 2: //is song lyric
                for (int i = 0; i < songLyricText.Count; i++)
                {
                    songLyricText[i].text = clues[index].GetSongLyricText(i);
                    songLyricText[i].gameObject.SetActive(true);

                    songLyricTextShadows[i].text = clues[index].GetSongLyricText(i);
                    songLyricTextShadows[i].gameObject.SetActive(true);
                }
                break;

            case 3: //is image
                Instantiate(clues[index].GetImage(), clueImageSpawnPoint.transform.position, Quaternion.identity, clueImageSpawnPoint.transform);
                break;

            default:
                for (int i = 0; i < clueText.Count; i++)
                {
                    clueText[i].text = clues[index].GetClueText();
                    clueText[i].gameObject.SetActive(true);
                }
                break;
        }
    }

    public void HardDeActivationAndHidePointNumber()
    {
        boardButtons[currentIndex].transform.GetChild(0).gameObject.SetActive(false);

        foreach (GameObject boardButton in boardButtons)
        { 
            if (boardButton.GetComponent<BoardButton>().WasJustInteractable())
            {
                boardButton.GetComponent<BoardButton>().InteractableFadeOut();
            }
        }
    }

    public void BackButtonPressed()
    {
        if (!wheelSpinButtonActive) { wheelSpinButton.gameObject.SetActive(false); }
        clueScreen.MoveOutClue();
    }


    public void SwitchCluesToDoubles()
    {
        beforeSwitchToDoubles = true;
        wheelSpinButton.gameObject.SetActive(false);

        if (!onlySingles)
        {
            gameState = GameState.DoubleBoard;

            doublesBoardButton.gameObject.SetActive(true);
            doublesBoardButton.BrightenAndInteractable();

            clues = cluesDouble;
        }
        
    }

    public void UpdateBoardTitlesAndButtons()
    {
        beforeSwitchToDoubles = false;

        foreach (CategoryTitleChange categoryTitleChange in categoryTitleChanges)
        {
            categoryTitleChange.SetDoubleTitle();
        }

        foreach (GameObject boardButton in boardButtons)
        {
            boardButton.GetComponent<BoardButton>().DoubleBoardReset();
            boardButton.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
