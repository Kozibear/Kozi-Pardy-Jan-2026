using KoziPardy.ColorManagement;
using UnityEngine;

namespace KoziPardy.GameState
{

    public class GameStateManager : MonoBehaviour
    {
        [Header("Static Variables")]
        public static GlobalGameState globalGameState = GlobalGameState.Single;

        [Header("Starting Game State")]
        [SerializeField] GlobalGameState startingGlobalGameState = GlobalGameState.Single;

        private void Awake() //needs to be awake so that objects that use globalGameState get the correct state
        {
            DontDestroyOnLoad(gameObject);

            globalGameState = startingGlobalGameState;
        }
    }

    public enum GlobalGameState { Single, Double, Final };

}