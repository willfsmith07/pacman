using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Animation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }

    public Sprite[] sprites;

    public float animationTime;

    public int animationFrame { get; private set; }

    public bool loop = true;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(nextAnimation), this.animationTime, this.animationTime);
    }

    private void nextAnimation()
    {
        this.animationFrame++;

        if (this.animationFrame >= this.sprites.Length && this.loop)
        {
            this.animationFrame = 0;
        }

        if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

    private void Restart()
    {
        this.animationFrame = -1;

        nextAnimation();

    }
}
