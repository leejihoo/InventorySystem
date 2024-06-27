using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Localization.Plugins.XLIFF.V12;

public class UIAnimationManager : Singleton<UIAnimationManager>
{
    [Range(0,10)]
    [SerializeField] private float animationDuration;

    [SerializeField] private Ease easeForScale;
    [SerializeField] private Ease easeForAlpha;
    [SerializeField] private Ease easeForRotation;

    public override void Awake()
    {
        base.Awake();
        if (animationDuration == 0)
        {
            animationDuration = 0.3f;
        }

        if (easeForScale == Ease.Unset)
        {
            easeForScale = Ease.OutBack;
        }
        
        if (easeForAlpha == Ease.Unset)
        {
            easeForAlpha = Ease.InCubic;
        }
        
        if (easeForRotation == Ease.Unset)
        {
            easeForRotation = Ease.Linear;
        }
    }

    public void ExecuteOpenUIAnimationByScale(Transform target)
    {
        target.DOScale(new Vector3(1, 1), animationDuration).SetEase(easeForScale);
    }

    public void ExecuteOpenUIAnimationByAlpha(Transform target)
    {
        target.GetComponent<CanvasGroup>().DOFade(1f, animationDuration).SetEase(easeForAlpha);
    }

    public void ExecuteOpenUIAnimationByRotation(Transform target)
    {
        target.DORotate(new Vector3(0, 360f), animationDuration, RotateMode.FastBeyond360).SetEase(easeForRotation);
    }
}
