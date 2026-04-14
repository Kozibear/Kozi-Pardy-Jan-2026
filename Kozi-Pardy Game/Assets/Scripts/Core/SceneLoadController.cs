using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace KoziPardy.Core
{
    public class SceneLoadController : MonoBehaviour
    {
        GameSettings gameSettings;

        private void Start()
        {
            gameSettings = GetComponent<GameSettings>();
        }

        public IEnumerator LoadSceneCoroutine(int sceneIndex, GlobalGameState nextGameState, bool naturalSceneSwitch)
        {
            gameSettings.SetGameState(nextGameState);
            gameSettings.SetNaturalSceneSwitch(naturalSceneSwitch);

            yield return SceneManager.LoadSceneAsync(sceneIndex);

            yield return null; //we wait a frame
        }
    }
}