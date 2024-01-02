using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesUI : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayNoColliderWarning()
    {
        anim.SetTrigger("NoColliderHit");
    }
}
