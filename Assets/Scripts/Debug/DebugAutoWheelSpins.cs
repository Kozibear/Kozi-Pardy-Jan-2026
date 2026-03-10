using TMPro;
using UnityEngine;

public class DebugAutoWheelSpins : MonoBehaviour
{
    [SerializeField] GameBoardManager gameBoardManager;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    private bool autoMode = false;
    public void AutoWheelSpins()
    {
        if (!autoMode)
        {
            gameBoardManager.ToggleAutoWheelSpins(true);
            autoMode = true;
            textMeshProUGUI.text = "MANUAL SPINS";
        }
        else
        {
            gameBoardManager.ToggleAutoWheelSpins(false);
            autoMode = false;
            textMeshProUGUI.text = "AUTO SPINS";
        }
    }
}
