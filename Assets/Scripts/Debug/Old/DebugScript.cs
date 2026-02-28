using UnityEngine;

public class DebugScript : MonoBehaviour
{
    [Header("Debug Options")]
    [SerializeField] bool skipIntro;
    [SerializeField] bool readyDoublesBoard;

    [Header("References")]
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject splashScreen;
    [SerializeField] GameObject categoryReveals;
    [SerializeField] SpriteRenderer blackBackgroundRenderer;

    void Start()
    {
        if (skipIntro)
        {
            gameManager.BoardBeforeWheelSpin();

            splashScreen.SetActive(false);
            categoryReveals.SetActive(false);

            Color color = new Color(0, 0, 0, 0);
            blackBackgroundRenderer.color = color;
        }

        if (readyDoublesBoard)
        {
            //gameManager.SwitchCluesToDoubles();
        }
    }
}
