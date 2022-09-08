using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public float duration;
    public bool start = false;
    public AnimationCurve animationCurveLine;
    public AnimationCurve animationCurveCircle;
    public AnimationCurve animationCurvePoint;
    public int type;
    private void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
            
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if(type == 2)
            {
                float strength = animationCurveCircle.Evaluate(elapsedTime / duration);
                transform.position = startPosition + Random.insideUnitSphere * strength;
            }
            if (type == 1)
            {
                float strength = animationCurveLine.Evaluate(elapsedTime / duration);
                transform.position = startPosition + Random.insideUnitSphere * strength;
            }
            if (type == 0)
            {
                float strength = animationCurvePoint.Evaluate(elapsedTime / duration);
                transform.position = startPosition + Random.insideUnitSphere * strength;
            }
            yield return null;
        }
        transform.position = startPosition;
    }
}
