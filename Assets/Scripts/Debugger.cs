using UnityEngine;  
using System.Collections;
public class Debugger : MonoBehaviour
{
    public bool showLogInConsole;
    void Start()
    {
        Conductor.instance.SendVisualBeat += VisualBeatHandler;
        Conductor.instance.BeatHit += BeatHitHandler;
        Conductor.instance.BeatMiss += BeatMissHandler;
        Conductor.instance.SendDebugBeat += DebugBeatHandler;
    }

    private void VisualBeatHandler(double songTime)
    {
        Log(string.Format("Received visual beat: {0:0.000}", songTime));
    }
    
    private void BeatHitHandler(double diff)
    {
        Grapher.Log(1, "beatHit", Color.green);
        StartCoroutine(SetGraphToZero("beatHit", Color.green));
        Log(string.Format("Received beat hit: {0:0.000}", Conductor.instance.songTime));
    }
    private void BeatMissHandler(double diff)
    {
        Grapher.Log(1, "beatMiss", Color.red);
        StartCoroutine(SetGraphToZero("beatMiss", Color.red));
        Log(string.Format("Received beat miss: {0:0.000}", Conductor.instance.songTime));
    }

    private void DebugBeatHandler(string name, double songTime, Color color)
    {
        Grapher.Log(0.5f, name, color);
        StartCoroutine(SetGraphToZero(name, color));
        Log(string.Format("Received debug beat: {0:0.000}", songTime));

    }

    private IEnumerator SetGraphToZero(string graph, Color color)
    {
        yield return new WaitForSeconds(0.001f);
        Grapher.Log(0, graph, color);
    }


    private void Log(string logMsg)
    {
        if(showLogInConsole)
            Debug.Log(logMsg);
    }
    void OnDestroy()
    {
        Conductor.instance.SendVisualBeat -= VisualBeatHandler;
        Conductor.instance.BeatHit -= BeatHitHandler;
        Conductor.instance.BeatMiss -= BeatMissHandler;
        Conductor.instance.SendDebugBeat -= DebugBeatHandler;
    }   
}