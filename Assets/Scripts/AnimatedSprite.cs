using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer sr { get; private set; }
    public Sprite[] sprites;
    public float animTime = 0.25f;
    public int animFrame { get; private set; }
    public bool loop = true;

    private void Awake() { sr = GetComponent<SpriteRenderer>(); }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), animTime, animTime);
    }

    private void Advance()
    {
        animFrame++;

        if (!sr.enabled) { return; }

        if (animFrame >= sprites.Length && loop) { animFrame = 0; }

        if (animFrame >= 0 && animFrame < sprites.Length) { sr.sprite = sprites[animFrame]; }
    }

    public void Restart()
    {
        animFrame = -1;

        Advance();
    }
}
