using UnityEngine;
public class VisualLatencyMetronome : MonoBehaviour
{
    public Transform stickTransform;
    public bool onLeft = true;
    private Quaternion leftRotation = Quaternion.Euler(new Vector3(0, 0, -45));
    private Quaternion rightRotation =  Quaternion.Euler(new Vector3(0, 0, 45));
    void Start()
    {
        Conductor.instance.SendVisualBeat += BeatHandler;
    }

    void Update()
    {
        if(onLeft)
        {
            stickTransform.localRotation = Quaternion.RotateTowards(stickTransform.localRotation, rightRotation, 
                90f * (float)Conductor.instance.GetLastFrameDSPTime()/(float)Conductor.instance.GetSecPerBeat());
        }
        else
        {
            stickTransform.localRotation = Quaternion.RotateTowards(stickTransform.localRotation, leftRotation, 
                90f * (float)Conductor.instance.GetLastFrameDSPTime()/(float)Conductor.instance.GetSecPerBeat());
        }
    }
    private void BeatHandler(double songTime)
    {
        if(onLeft)
        {
            stickTransform.localRotation = Quaternion.Euler(0, 0, 45);
        }
        else
        {
            stickTransform.localRotation = Quaternion.Euler(0, 0, -45);
        }
        onLeft = !onLeft;
    }

    void OnDestroy()
    {
        Conductor.instance.SendVisualBeat -= BeatHandler;
    }
}