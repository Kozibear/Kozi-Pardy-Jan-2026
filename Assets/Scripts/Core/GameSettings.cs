using UnityEngine;

namespace KoziPardy.Core
{
    public class GameSettings : MonoBehaviour
    {
        [Header("Static Variables")]
        public static GlobalGameState globalGameState = GlobalGameState.Single;
        public static GlobalGameEndState globalGameEndState = GlobalGameEndState.EndAtSingle;
        public static bool wheelSpinGame = true;
        public static bool colorChangeGame = true;
        public static bool naturalSceneSwitch = false;
        public static GameObject theOnlyGameManager;

        [Header("Settings")]
        [SerializeField] GlobalGameState thisGamesStartState = GlobalGameState.Single;
        [SerializeField] GlobalGameState thisGamesCurrentState = GlobalGameState.Single;
        [SerializeField] GlobalGameEndState thisGamesEndState = GlobalGameEndState.EndAtSingle;
        [SerializeField] bool isWheelSpinGame = true;
        [SerializeField] bool isColorChangeGame = true;

        //In the future, I should rework this script so that the script, rather than the variables are static, meaning we will have a getter and setter for each variable

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
                wheelSpinGame = isWheelSpinGame;
                colorChangeGame = isColorChangeGame;
                naturalSceneSwitch = false;
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

        public void SetNaturalSceneSwitch(bool state)
        {
            naturalSceneSwitch = state;
        }
    }

    public enum GlobalGameState { Single, Double, Final };
    public enum GlobalGameEndState { EndAtSingle, EndAtDouble, EndAtFinal };

}