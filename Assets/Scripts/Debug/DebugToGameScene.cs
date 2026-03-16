using UnityEngine;

namespace KoziPardy.Core
{
    public class DebugToGameScene : MonoBehaviour
    {
        public void StartSingleScene(int sceneIndex)
        {
            StartCoroutine(GameSettings.theOnlyGameManager.GetComponent<SceneLoadController>().LoadSceneCoroutine(sceneIndex, GlobalGameState.Single, false));
        }

        public void StartDoubleScene(int sceneIndex)
        {
            StartCoroutine(GameSettings.theOnlyGameManager.GetComponent<SceneLoadController>().LoadSceneCoroutine(sceneIndex, GlobalGameState.Double, false));
        }

        public void StartFinalScene(int sceneIndex)
        {
            StartCoroutine(GameSettings.theOnlyGameManager.GetComponent<SceneLoadController>().LoadSceneCoroutine(sceneIndex, GlobalGameState.Final, false));
        }
    }
}