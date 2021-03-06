﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrooveObject : MonoBehaviour
{
    public Animator anim;
    [SerializeField]
    private Material defaultMat;
    
    [SerializeField]
    private Material beatMat;
    private MeshRenderer mesh;
    [SerializeField]
    private float MatChangeDuration = 0.2f;
    public bool grooveEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        Conductor.instance.SendVisualBeat += BeatHandler;
        mesh = GetComponent<MeshRenderer>();
    }
    public void BeatHandler(double songTime)
    {
        if(grooveEnabled)
            StartCoroutine(Beat());
    }

    
    private IEnumerator Beat()
    {
        mesh.material = beatMat;
        anim.SetTrigger("Groovin");
        yield return new WaitForSeconds(MatChangeDuration);
        mesh.material = defaultMat;
    }

    void OnDestroy()
    {
        Conductor.instance.SendVisualBeat -= BeatHandler;
    }
}
