using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashDamage : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FlashTrigger()
    {
        anim.SetTrigger("hit");
    }
}
