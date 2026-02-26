using UnityEngine;

public class WheelArrow : MonoBehaviour
{
    [Header("WheelSpin Board Selections")]
    [SerializeField] WheelSpinBoardSelections wheelSpinBoardSelections;

    private bool canTriggerSelection = false;

    public void SetTriggerSelection(bool state) { canTriggerSelection = state; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canTriggerSelection)
        {
            wheelSpinBoardSelections.LightUpBoardParts(collision.GetComponent<WheelSegment>().GetCurrentSegment());
        }
    }
}
