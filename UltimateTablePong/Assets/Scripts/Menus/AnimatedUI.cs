using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedUI : MonoBehaviour {

    private static readonly float TIME_BETWEEN_FRAMES = 0.041f;

    [SerializeField] private Sprite[] sprites;
    private Image image;
    private int currentFrame;
    private float timer = 0.0f;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= TIME_BETWEEN_FRAMES)
        {
            timer -= TIME_BETWEEN_FRAMES;
            currentFrame = (currentFrame + 1) % sprites.Length;

            image.sprite = sprites[currentFrame];
        }
    }
}
