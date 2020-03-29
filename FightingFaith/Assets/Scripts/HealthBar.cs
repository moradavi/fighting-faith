using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public RectTransform healthBarRectTransform;
    public Health health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 healthBarScale = new Vector2((float)health.CurrentHealth/health.maxHealth, 1);
        healthBarRectTransform.localScale = healthBarScale;
    }
}
