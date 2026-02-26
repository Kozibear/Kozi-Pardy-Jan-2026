using System.Collections.Generic;
using UnityEngine;

namespace KoziPardy.ColorManagement
{
    public class GlobalColorManager : MonoBehaviour
    {
        [Header("Static Variables")]
        public static GlobalColorState globalColorState = GlobalColorState.Blue;
        public static readonly float desiredDurationInSeconds = 0.9f;

        [Header("Board Buttons")]
        [SerializeField] List<BoardClueStateControl> boardClueStateControls;

        [Header("Category Buttons")]
        [SerializeField] List<BoardClueColorChange> categoryColorChanges;

        [Header("The Board")]
        [SerializeField] BoardSpriteColorChange boardSpriteColorChange;

        [Header("The Wheel")]
        [SerializeField] WheelColorChange wheelColorChange;

        [Header("Background")]
        [SerializeField] BackgroundColorChange backgroundColorChange;

        public void Start()
        {
            globalColorState = GlobalColorState.Blue;
        }

        public void ChangeGlobalColor()
        {
            foreach (BoardClueStateControl clue in boardClueStateControls)
            {
                clue.StartGradualColorChange();
            }

            foreach (BoardClueColorChange category in categoryColorChanges)
            {
                category.StartGradualColorChange();
            }

            boardSpriteColorChange.StartGradualColorChange();

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

            foreach (BoardClueColorChange categoryColorChange in categoryColorChanges)
            {
                categoryColorChange.InstantColorChange(BoardClueColorChange.ColorValue.Dark);
            }
        }
    }

    public enum GlobalColorState { Blue, Orange, Purple };
}