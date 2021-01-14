﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainButtonDryer : MonoBehaviour
{
    Animator animator;
    AudioSource sound;

    const string PRESSED = "ButtonPress";
    const string IDLE = "ButtonIdle";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        sound.Play();
        ChangeAnimationState(PRESSED);
        ChangeAnimationState(IDLE);
        gameObject.GetComponentInParent<Dryer>().DrainOnClick();
    }

    void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }
}
