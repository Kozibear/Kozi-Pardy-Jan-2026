using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Persistence : MonoBehaviour
{
    [SerializeField] GameObject persistentObject1;
    [SerializeField] GameObject persistentObject2;
    [SerializeField] GameObject persistentObject3;

    private void Start()
    {
        DontDestroyOnLoad(persistentObject1);
        DontDestroyOnLoad(persistentObject2);
        DontDestroyOnLoad(persistentObject3);
    }

    private void Update()
    {
    }
}
