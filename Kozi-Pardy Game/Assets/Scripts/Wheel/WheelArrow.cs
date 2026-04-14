using UnityEngine;

public class WheelArrow : MonoBehaviour
{
    [Header("WheelSpin Board Selections")]
    [SerializeField] WheelSpinBoardSelections wheelSpinBoardSelections;

    private bool canTriggerSelection = false;

    public void SetTriggerSelection(bool state) { canTriggerSelection = state; }

    private void OnTriggerEnter(Collider collision)
    {
        if (canTriggerSelection)
        {
            collision.GetComponent<SpriteFade>().InstantPulseOut();
            wheelSpinBoardSelections.LightUpBoardParts(collision.GetComponent<WheelSegment>().GetCurrentSegment(), collision.GetComponent<WheelSegment>().GetPartNumber());
        }
    }

}
