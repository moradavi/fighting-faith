using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashDamage : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    public Material defaultMaterial;
    public Material flashMaterial;
    bool isFlashing;
    public float flashTime;
    float timer;

    private void Start()
    {
        isFlashing = false;
    }

    private void Update()
    {
        if(isFlashing == true)
        {
            timer += Time.deltaTime;
            if(timer > flashTime)
            {
                foreach (SpriteRenderer sp in spriteRenderers)
                {
                    sp.material = defaultMaterial;
                }
                isFlashing = false;
                timer = 0;
            }
        }
    }

    public void FlashTrigger()
    {

        isFlashing = true;
        foreach(SpriteRenderer sp in spriteRenderers)
        {
            sp.material = flashMaterial; 
        }
    }
}
