using UnityEngine;

namespace KoziPardy.Core
{
    public class GameSettings : MonoBehaviour
    {
        [Header("Static Variables")]
        public static GlobalGameState globalGameState = GlobalGameState.Single;
        public static GlobalGameEndState globalGameEndState = GlobalGameEndState.EndAtSingle;
        public static bool WheelSpinGame = true;
        public static bool ColorChangeGame = true;
        public static GameObject theOnlyGameManager;

        [Header("Settings")]
        [SerializeField] GlobalGameState thisGamesStartState = GlobalGameState.Single;
        [SerializeField] GlobalGameState thisGamesCurrentState = GlobalGameState.Single;
        [SerializeField] GlobalGameEndState thisGamesEndState = GlobalGameEndState.EndAtSingle;
        [SerializeField] bool isWheelSpinGame = true;
        [SerializeField] bool isColorChangeGame = true;

        private void Awake() //needs to be awake so that objects that use globalGameState get the correct state
        {
            DontDestroyOnLoad(gameObject);

            if (theOnlyGameManager != null) //if we already have a static reference to this game object, we destroy any subsequent copies
            {
                Destroy(gameObject);
            }
            else//otherwise, we assign this gameobject to be the static reference, and fill out the rest of the static variables
            {
                theOnlyGameManager = gameObject;

                globalGameState = thisGamesCurrentState = thisGamesStartState;
                globalGameEndState = thisGamesEndState;
                WheelSpinGame = isWheelSpinGame;
                ColorChangeGame = isColorChangeGame;
            }
        }

        private void OnApplicationQuit() //needed as my current unity's editor settings don't reset static values
        {
            theOnlyGameManager = null;
        }

        public void SetGameState(GlobalGameState state)
        {
            globalGameState = thisGamesCurrentState = state;
        }
    }

    public enum GlobalGameState { Single, Double, Final };
    public enum GlobalGameEndState { EndAtSingle, EndAtDouble, EndAtFinal };

}