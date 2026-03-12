using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace KoziPardy.Core
{
    public class SceneLoadController : MonoBehaviour
    {
        public IEnumerator LoadSceneCoroutine(int sceneIndex, GlobalGameState nextGameState)
        {
            GetComponent<GameSettings>().SetGameState(nextGameState);

            yield return SceneManager.LoadSceneAsync(sceneIndex);

            yield return null; //we wait a frame
        }
    }
}