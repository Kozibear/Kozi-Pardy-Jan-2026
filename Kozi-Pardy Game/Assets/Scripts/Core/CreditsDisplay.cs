using UnityEngine;

namespace KoziPardy.Core
{
    public class CreditsDisplay : MonoBehaviour
    {
        void Start()
        {
            if (GameSettings.globalGameState != GlobalGameState.Single)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}