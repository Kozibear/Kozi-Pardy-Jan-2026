using System;
using System.Collections.Generic;
using UnityEngine;

public class WheelSegment : MonoBehaviour
{
    [SerializeField] List<GameObject> segmentNames;
    [SerializeField] List<GameObject> segmentNamesDouble;

    private int currentSegment;

    public void DisplaySegmentName()
    {
        Instantiate(segmentNames[currentSegment], transform.position, transform.rotation, transform);
    }

    public void SetCurrentSegment(int segmentIndex)
    {
        currentSegment = segmentIndex;
    }

    public int GetCurrentSegment()
    {
        return currentSegment;
    }

    public void DestroyChild()
    {
        if (transform.childCount > 0) Destroy(transform.GetChild(0).gameObject);
    }
    public void SwitchToDoubleSegments()
    {
        segmentNames = segmentNamesDouble;
    }
}
