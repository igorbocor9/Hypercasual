using UnityEngine;
using System.Collections.Generic;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimatorSetup> animatorSetups;

    public enum AnimatorType
    {
        IDLE,
        RUN,
        DEAD
    }

    public void Play(AnimatorType type)
    {
        foreach (var setup in animatorSetups)
        {
            if (setup.type == type)
            {
                animator.SetTrigger(setup.trigger);
                break;
            }
        }
    }
}   

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimatorType type;
    public string trigger;
}
