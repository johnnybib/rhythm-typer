using UnityEngine;
public class Debugger : MonoBehaviour
{
    public bool doDebug;
    void Start()
    {
        Conductor.instance.SendVisualBeat += VisualBeatHandler;
        Conductor.instance.SendTimedBeat += TimedBeatHandler;
        if(TypingInput.instance)
            TypingInput.instance.SendBeat += TypingBeatHandler;
    }
    private void TypingBeatHandler(double songTime)
    {
        Log(string.Format("Received typing beat: {0:0.000}", songTime));
    }
    private void VisualBeatHandler(double songTime)
    {
        Log(string.Format("Received visual beat: {0:0.000}", songTime));
    }
    
    private void TimedBeatHandler(double songTime)
    {
        Log(string.Format("Received timed beat: {0:0.000}", songTime));
    }

    private void Log(string logMsg)
    {
        if(doDebug)
            Debug.Log(logMsg);
    }

    
}