using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textBox;
    public int hits = 0;
    public int misses = 0;
    void Start()
    {
        TypingInput.instance.CharCorrect += CharCorrectHandler;
        TypingInput.instance.CharIncorrect += CharIncorrectHandler;
        TypingBeatChecker.instance.BeatMiss += BeatMissHandler;
    }
    public void CharCorrectHandler()
    {
        hits++;
        UpdateUI();
    }

    public void CharIncorrectHandler()
    {
        misses++;
        UpdateUI();
    }

    public void BeatMissHandler()
    {
        misses++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        textBox.text = string.Format("Hits {0}, Misses {1}", hits, misses);
    }

    void OnDestroy()
    {
        TypingInput.instance.CharCorrect -= CharCorrectHandler;
        TypingInput.instance.CharIncorrect -= CharIncorrectHandler;
        TypingBeatChecker.instance.BeatMiss -= BeatMissHandler;
    }

}

