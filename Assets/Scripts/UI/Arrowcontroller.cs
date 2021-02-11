using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowcontroller : MonoBehaviour
{
    public Animation anim;

    private bool shouldplay;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    private void Update()
    {
        if (shouldplay && anim.isPlaying == false)
        {
            anim.Play();
        }
    }

    public void AnimatorChange(bool Play)
    {
        shouldplay = Play;
        if (Play && anim.isPlaying == false)
        {
            anim.Play();  
        }
        else
            anim.Stop();
    }
}
