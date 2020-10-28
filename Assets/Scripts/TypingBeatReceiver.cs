
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TypingBeatReceiver : MonoBehaviour
{
    public Animator anim;
    [SerializeField]
    private Material defaultMat;
    
    [SerializeField]
    private Material beatMat;
    [SerializeField]
    private Material missMat;
    private MeshRenderer mesh;

    [SerializeField]
    private float MatChangeDuration;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        TypingInput.instance.CharCorrect += CharCorrectHandler;
        TypingInput.instance.CharIncorrect += CharIncorrectHandler;
        Conductor.instance.BeatMiss += BeatMissHandler;
    }
    public void CharCorrectHandler()
    {
        mesh.material = beatMat;
        anim.SetTrigger("Groovin");
        StartCoroutine(BeatHit());
    }

    public void CharIncorrectHandler()
    {
        BeatMissHandler(1);
    }
    public void BeatMissHandler(double diff)
    {
        mesh.material = missMat;
        anim.SetTrigger("Miss");
        StartCoroutine(BeatMiss());
    }


    private IEnumerator BeatHit()
    {
        yield return new WaitForSeconds(MatChangeDuration);
        mesh.material = defaultMat;
    }
    
    private IEnumerator BeatMiss()
    {
        yield return new WaitForSeconds(MatChangeDuration);
        mesh.material = defaultMat;
    }

    void OnDestroy()
    {
        TypingInput.instance.CharCorrect -= CharCorrectHandler;
        TypingInput.instance.CharIncorrect -= CharIncorrectHandler;
        Conductor.instance.BeatMiss -= BeatMissHandler;
    }
}