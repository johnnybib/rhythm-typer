using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Animator anim;
    private float shakeMagnitude;
    private IEnumerator shakingCoroutine;
    void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void Zoom()
    {
        anim.SetTrigger("Zoom");
    }
    public void Shake(float magnitude)
    {

        //Check if new shake is more than previous
        if(magnitude >= shakeMagnitude)
        {
            //If it is, overwrite old shake with bigger shake
            shakeMagnitude = magnitude;
            StartCoroutine(ShakeEnd(magnitude));
        }
    }

    private IEnumerator ShakeEnd(float shakeTime)
    {
        float timer = 0;
        Vector3 originalPos = transform.position;
        while(timer < shakeTime)
        {
            transform.position += Random.insideUnitSphere * shakeMagnitude;
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        transform.position = originalPos;
        shakeMagnitude = 0;
    }
}
    