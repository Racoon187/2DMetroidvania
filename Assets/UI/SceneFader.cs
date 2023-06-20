using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    Animator _animator;
    int faderID;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        faderID = Animator.StringToHash("Fade");

        GameManager.RegisterScnenFader(this);
    }

    public void FadeOut()
    {
        _animator.SetTrigger(faderID);
    }
}
