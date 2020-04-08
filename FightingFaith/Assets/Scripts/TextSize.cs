using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSize : MonoBehaviour
{
    public Text text;

    void Start()
    {
        //Checks player preferences and sets the text size to the saved size
        this.gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("NarrativeSize");
    }

    public void ScaleText(Slider slider)
    {
        //Adjusts the font size according to the slider parameter
        text.fontSize = (int)slider.value;
    }

    private void OnDestroy()
    {
        //Sets the int value to the current font size
        PlayerPrefs.SetInt("NarrativeSize", text.fontSize);
        //Saves the current preferences
        PlayerPrefs.Save();
    }
}
