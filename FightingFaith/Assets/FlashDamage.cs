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
    public AudioSource slashSound;

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
                slashSound.Play();
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
