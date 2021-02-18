using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DestroyAnimatedObject : MonoBehaviour
{
    private Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();

        var clipInfo = myAnimator.GetCurrentAnimatorClipInfo(0);
        StartCoroutine(DestroySelf(clipInfo.Length / 2));
    }

    private IEnumerator DestroySelf(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyImmediate(gameObject);
    }
}
