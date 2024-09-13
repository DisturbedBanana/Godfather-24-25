using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Anim : MonoBehaviour
{
    private bool isSelected = true;
    private Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    
    private void Update()
    {
        if (isSelected && GameManager.instance.image.isVisible)
        {
            AnimStart();
            isSelected = false;
        }
    }

    private void AnimStart()
    {
        anim.SetTrigger("Cry");
        DOVirtual.DelayedCall(1.5f,() => isSelected = true);
    }
    
}
