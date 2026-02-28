using TMPro;
using UnityEngine;

public class DebugAutoWheelSpins : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    private bool autoMode = false;
    public void AutoWheelSpins()
    {
        if (!autoMode)
        {
            gameManager.ToggleAutoWheelSpins(true);
            autoMode = true;
            textMeshProUGUI.text = "MANUAL SPINS";
        }
        else
        {
            gameManager.ToggleAutoWheelSpins(false);
            autoMode = false;
            textMeshProUGUI.text = "AUTO SPINS";
        }
    }
}
