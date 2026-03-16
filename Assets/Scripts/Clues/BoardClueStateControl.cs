using KoziPardy.ColorManagement;
using UnityEngine;

namespace KoziPardy.Core
{
    public class BoardClueStateControl : MonoBehaviour
    {
        [SerializeField] int number = 0;

        [SerializeField] BoardClueColorChange boardClueColorChange;

        [SerializeField] TextColorFade pointValueText;

        [SerializeField] CategoryHeaderStateControl thisCluesCategory;

        private bool hasBeenClicked = false;

        private void Start()
        {
            if (GameSettings.wheelSpinGame) pointValueText.InstantDarkenToExactColor();
            boardClueColorChange.SetStartingColor(GameSettings.wheelSpinGame);
        }

        public void StartNextColorShift()
        {
            boardClueColorChange.StartNexColorShift(hasBeenClicked);
        }

        public void InstantDarken()
        {
            if (!hasBeenClicked)
            {
                pointValueText.InstantDarkenToExactColor();
                boardClueColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Dark, false, false);
                thisCluesCategory.InstantDarken(); //<-- must implement this again
            }
        }

        public void InstantLighten()
        {
            if (!hasBeenClicked)
            {
                pointValueText.InstantBrightenToExactColor();
                boardClueColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Bright, false, false);
                thisCluesCategory.InstantLighten();
            }
        }

        public void GradualDarkenIfOld()
        {
            if (hasBeenClicked)
            {
                boardClueColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Dark, true, true);
            }
        }

        public void GradualBrightenIfOld()
        {
            if (hasBeenClicked)
            {
                boardClueColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Bright, true, true);
            }
        }

        public void FinalClueSetup()
        {
            //we gradually brighten the button
            if (GameSettings.wheelSpinGame) pointValueText.GradualBrightenToExactColor();
            thisCluesCategory.GradualLightenIfFinal();
            boardClueColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Bright, true, false);

            //We also demark that this button is the final button in the BoardClueMovement script
            GetComponent<BoardClueMovement>().SetIsFinalClue(true);
        }

        public int GetNumber() { return number; }

        public void SetHasBeenClicked(bool value)
        {
            hasBeenClicked = value;
            if (!GameSettings.wheelSpinGame) thisCluesCategory.ReduceActiveCluesInThisCategory();
        }

        public bool GetHasBeenClicked() { return hasBeenClicked; }
    }
}