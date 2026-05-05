using UnityEngine;
using System.Collections;

public class AnimationSci : MonoBehaviour
{
    public Sprite[] frames;   
    public float frameRate = 0.2f; // задержка между кадрми
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        int index = 0;
        while (true)
        {
            sr.sprite = frames[index];
            index = (index + 1) % frames.Length;
            yield return new WaitForSeconds(frameRate);
        }
    }
}
