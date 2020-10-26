using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textBox;

    public Color32 completedColor;
    public Color32 uncompletedColor;
    void Start()
    {
        TypingInput.instance.WordUpdated += UpdateUI;
        TypingInput.instance.WordCompleted += WordCompletedHandler;
    }

    public void UpdateUI(string completed, string uncompleted) 
    {
        textBox.text = string.Format("<color=#{0}>{1}</color><color=#{2}>{3}</color>", 
            ColorToHex(completedColor), completed, ColorToHex(uncompletedColor), uncompleted);
    }
    public void WordCompletedHandler(string word)
    {
        ClearUI();
    }
    public void ClearUI() 
    {
        textBox.text = "";
    }
    string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    void OnDestroy()
    {
        TypingInput.instance.WordUpdated -= UpdateUI;
        TypingInput.instance.WordCompleted -= WordCompletedHandler;
    }
}

