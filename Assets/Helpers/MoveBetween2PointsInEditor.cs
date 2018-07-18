using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBetween2PointsInEditor : MonoBehaviour
{

    float count = 0.0f;
    private GameObject GameObject1;
    private GameObject GameObject2;
    public GameObject Start_GameObject;
    public GameObject Stop_GameObject;

    private Vector3 position1
    {
        get { return GameObject1.transform.position; }
    }
    private Vector3 position2
    {
        get { return GameObject2.transform.position; }
    }
    private float travelTime = 0;
    private bool started = false;
    private float duration = 2.0f;
    private float journey = 0.0f;

    public AnimationCurve animationCurve;


    void Update()
    {
        if (GameObject1 == null && Start_GameObject != null)
            GameObject1 = Start_GameObject;
        if (GameObject2 == null && Stop_GameObject != null)
            GameObject2 = Stop_GameObject;

        travelTime += Time.deltaTime;

        if (started == false)
        {
            journey = 0f;
            started = true;
        }
        
        if (journey <= duration)
        {

            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);

            float curvePercent = animationCurve.Evaluate(percent);
            transform.gameObject.transform.position = Vector3.LerpUnclamped(position1, position2, curvePercent);
        }

        if (travelTime >= duration)
        {
            travelTime = 0;
            started = false;
            SwapGameObjects();
            //UnityEditor.EditorGUIUtility.PingObject(this); //som att trycka F på ett object
        }
    }



    private void SwapGameObjects()
    {
        GameObject go = GameObject1;
        GameObject1 = GameObject2;
        GameObject2 = go;
    }
}
