using UnityEngine;

public class AnimLop : MonoBehaviour
{ 
    [SerializeField] private AnimationClip animationClip; // сюда вставляешь анимацию
    private Animation anim;

    void Start()
    {
        anim = gameObject.AddComponent<Animation>();

        if (animationClip != null)
        {
            anim.AddClip(animationClip, animationClip.name);
            anim.clip = animationClip;
            anim[animationClip.name].wrapMode = WrapMode.Loop; // делает анимацию бесконечной
            anim.Play(animationClip.name);
        }
        else
        {
            Debug.LogWarning("Не установлена анимация в инспекторе!", gameObject);
        }
    }
}
