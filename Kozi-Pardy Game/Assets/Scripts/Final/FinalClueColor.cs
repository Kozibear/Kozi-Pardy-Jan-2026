using UnityEngine;

public class FinalClueColor : MonoBehaviour
{
    [Header("Highlight Color")]
    [SerializeField] Color blueHighlight;

    [Header("The Cube's Mesh Renderer")]
    [SerializeField] MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer.material.color = blueHighlight;
    }

}
