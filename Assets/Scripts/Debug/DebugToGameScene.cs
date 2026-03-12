using System.Collections;
using UnityEngine;

namespace KoziPardy.Core
{
    public class DebugToGameScene : MonoBehaviour
    {
        public void StartSingleScene(int sceneIndex)
        {
            StartCoroutine(GameSettings.theOnlyGameManager.GetComponent<SceneLoadController>().LoadSceneCoroutine(sceneIndex, GlobalGameState.Single));
        }

        public void StartDoubleScene(int sceneIndex)
        {
            StartCoroutine(GameSettings.theOnlyGameManager.GetComponent<SceneLoadController>().LoadSceneCoroutine(sceneIndex, GlobalGameState.Double));
        }

        public void StartFinalScene(int sceneIndex)
        {
            StartCoroutine(GameSettings.theOnlyGameManager.GetComponent<SceneLoadController>().LoadSceneCoroutine(sceneIndex, GlobalGameState.Final));
        }
    }
}