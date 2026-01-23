using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Blur : MonoBehaviour
{
    [SerializeField] Volume volume;
    private DepthOfField depthOfField;

    [Header("Blur Speeds")]
    [SerializeField] float blurSpeed;
    [SerializeField] float unBlurSpeed;

    private float blurThreshold = 0.1f;
    private float unBlurThreshold = 100f; //2,8f

    private bool canBlur = false;
    private bool canUnBlur = false;
    private float currentDOFvalue;

    void Start()
    {
        volume.profile.TryGet(out depthOfField);
    }

    private void Update()
    {
        if (canBlur)
        {
            currentDOFvalue -= Time.deltaTime * blurSpeed;
            depthOfField.focusDistance.value = currentDOFvalue; 

            if (depthOfField.focusDistance.value <= blurThreshold) canBlur = false;
        }

        if (canUnBlur)
        {
            currentDOFvalue += Time.deltaTime * unBlurSpeed;
            depthOfField.focusDistance.value = currentDOFvalue;

            if (depthOfField.focusDistance.value >= unBlurThreshold) canUnBlur = false;
        }
    }

    [ContextMenu("Test BlurBackground")]
    public void BlurBackground()
    {
        currentDOFvalue = depthOfField.focusDistance.value;
        canBlur = true;
    }

    [ContextMenu("Test UnBlurBackground")]
    public void UnBlurBackground()
    {
        currentDOFvalue = depthOfField.focusDistance.value;
        canUnBlur = true;
    }
}