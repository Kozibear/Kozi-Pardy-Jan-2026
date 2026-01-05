using UnityEngine;

public class SpriteFade : Fade
{
    [Header("Sprite")]
    [SerializeField] SpriteRenderer spriteRenderer;

    protected override Color GetColor() { return spriteRenderer.color; }

    protected override void SetColor(Color updatedColor) { spriteRenderer.color = updatedColor; }
}
