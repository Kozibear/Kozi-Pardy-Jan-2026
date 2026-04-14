using KoziPardy.Core;
using System.Collections.Generic;
using UnityEngine;

namespace KoziPardy.ColorManagement
{
    public class GlobalColorManager : MonoBehaviour
    {
        [Header("Static Variables")]
        public static GlobalColorState globalColorState = GlobalColorState.Blue;

        public static readonly float desiredDurationInSeconds = 0.85f;

        [Header("Board Buttons")]
        [SerializeField] List<BoardClueStateControl> boardClueStateControls;

        [Header("The Categories")]
        [SerializeField] List<GameObject> categories;

        [Header("The Board")]
        [SerializeField] List<BoardSpriteColorChange> boardSpriteColorChanges;

        [Header("Background")]
        [SerializeField] BackgroundColorChange backgroundColorChange;

        private bool canColorChange = true;

        public void Start()
        {
            canColorChange = GameSettings.colorChangeGame;
            globalColorState = GlobalColorState.Blue;
        }

        public void ChangeGlobalColor()
        {
            if (!canColorChange) return;

            foreach (BoardClueStateControl clue in boardClueStateControls)
            {
                clue.StartNextColorShift();
            }

            foreach (GameObject category in categories)
            {
                category.GetComponent<BoardClueColorChange>().StartNexColorShift(false);
            }

            foreach (BoardSpriteColorChange boardSpriteColorChange in boardSpriteColorChanges)
            {
                boardSpriteColorChange.StartGradualColorChange();
            }

            backgroundColorChange.StartGradualColorChange();


            if (globalColorState == GlobalColorState.Blue)
            {
                globalColorState = GlobalColorState.Orange;
            }
            else if (globalColorState == GlobalColorState.Orange)
            {
                globalColorState = GlobalColorState.Purple;
            }
            else if (globalColorState == GlobalColorState.Purple)
            {
                globalColorState = GlobalColorState.Blue;
            }
        }

        public void DarkenAllOtherBoardButtons(int buttonToWhiteList)
        {
            for (int i = 0; i < boardClueStateControls.Count; i++)
            {
                if (i == buttonToWhiteList) continue;

                boardClueStateControls[i].InstantDarken();
            }

            foreach (GameObject category in categories)
            {
                category.GetComponent<CategoryHeaderStateControl>().DarkenJustCategoryName();
                category.GetComponent<BoardClueColorChange>().ColorBrightenDarken(BoardClueColorChange.ColorValue.Dark, false, false);
            }
        }
    }

    public enum GlobalColorState { Blue, Orange, Purple };
}