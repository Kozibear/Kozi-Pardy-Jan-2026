using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugToGameScene : MonoBehaviour
{
    [SerializeField] int sceneIndex;

    public void LoadSpecifiedScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
