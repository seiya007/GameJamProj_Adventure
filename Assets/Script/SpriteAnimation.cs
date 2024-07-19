using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] renderer;

    [SerializeField]
    private SpriteAnimationPosData posData;

    private IEnumerator animator = null;
    private const float ANIMATION_DURATION = 0.1f;
    public enum AnimState
    {
        Idle,
        Walk,
        Run
    }
    public AnimState animState;
    private AnimState prev_animState;

    private void Show(int targetIdx = 0)
    {
        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].gameObject.SetActive(i == targetIdx);
            renderer[i].transform.localPosition = posData.GetPos(i);
        }
    }

    private void OnEnable()
    {
        // animator = WalkAnimator(ANIMATION_DURATION);
        animator = IdleAnimator();
        StartCoroutine(animator);
    }

    private void Update()
    {
        if(animState != prev_animState)
        {
            AnimChange(animState);
            Debug.Log("アニメーション変更");
        }

        prev_animState = animState;
    }

    private IEnumerator WalkAnimator(float interval = 1.0f)
    {
        var index = 0;
        animState = AnimState.Walk;

        while(true)
        {
            Show(index);
            yield return new WaitForSeconds(interval);

            index++;
            if(index >= renderer.Length)
                index = 0;
        }
    }

    private IEnumerator IdleAnimator()
    {
        int walkSpriteNumber = 0;
        animState = AnimState.Idle;
        Show(walkSpriteNumber);
        animator = null;
        yield break;
    }

    public void AnimChange(AnimState state)
    {
        //直前のアニメーションを止める
        if(animator != null)
        {
            StopCoroutine(animator);
            animator = null;
        }

        switch(state)
        {
            case AnimState.Idle:
                animator = IdleAnimator();
                break;
            case AnimState.Walk:
                animator = WalkAnimator(ANIMATION_DURATION);
                break;
        }
        StartCoroutine(animator);
    }
}
