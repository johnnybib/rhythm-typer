using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ComboMeter : MonoBehaviour
{
    public Animator anim;
    public TextMeshProUGUI comboMeterText;
    private int beatsSinceLastPress;
    private int comboLevel;
    void Start()
    {
        Conductor.instance.SendVisualBeat += BeatReceivedHandler;
        TypingInput.instance.CharCorrect += CharCorrectHandler;
        TypingInput.instance.CharIncorrect += CharIncorrectHandler;
        TypingBeatChecker.instance.BeatMiss += BeatMissHandler;    
        beatsSinceLastPress = 0;
        UpdateComboMeter(0);
    }

    public void BeatReceivedHandler()
    {
        anim.SetTrigger("Groovin");
        beatsSinceLastPress++;
        if(beatsSinceLastPress > 1)
        {
            ResetComboMeter();
        }
    }
    public void CharCorrectHandler()
    {
        UpdateComboMeter(comboLevel + 1 * (2-beatsSinceLastPress));
        beatsSinceLastPress = 0;
    }

    public void CharIncorrectHandler()
    {
        ResetComboMeter();
    }
    public void BeatMissHandler()
    {
        ResetComboMeter();
    }

    private void ResetComboMeter()
    {
        UpdateComboMeter(0);
        beatsSinceLastPress = 0;
    }

    private void UpdateComboMeter(int level)
    {
        comboLevel = level;
        comboMeterText.text = string.Format("{0}X COMBO", comboLevel);
    }

}
