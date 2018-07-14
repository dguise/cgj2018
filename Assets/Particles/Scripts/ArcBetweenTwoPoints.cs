using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcBetweenTwoPoints : MonoBehaviour {

    float count = 0.0f;
    private GameObject GameObject1;
    private GameObject GameObject2;
    public GameObject Start_GameObject;
    public GameObject Stop_GameObject;

    private float heightOfCurve = 10.0f;
    private Vector3 position1
    {
        get { return GameObject1.transform.position; }
    }
    private Vector3 position2
    {
        get { return GameObject2.transform.position; }
    }
    public float lerpTime = 0.5f;
    private float travelTime = 0;
    public float WaitAfterTravelTime = 5.0f;

    private GameObject particleTrail;
    private GameObject particleFire;

    void Start()
    {
        GameObject1 = Start_GameObject;
        GameObject2 = Stop_GameObject;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (i == 0)
                particleTrail = this.transform.GetChild(i).gameObject;
            if (i == 1)
                particleFire = this.transform.GetChild(i).gameObject;

        }
    }

    void Update()
    {
        travelTime += Time.deltaTime;
        if (count < 1)
        {
            count += lerpTime * Time.deltaTime;
            Vector3 measurePoint = CurvesHighestPoint(position1, position2);
            Vector3 m1 = Vector3.Lerp(position1, measurePoint, count);
            Vector3 m2 = Vector3.Lerp(measurePoint, position2, count);
            this.gameObject.transform.position = Vector3.Lerp(m1, m2, count);

            particleTrail.SetActive(true);
            particleFire.SetActive(false);
        }
        else
        {
            particleTrail.SetActive(false);
            particleFire.SetActive(true);
        }

        if (travelTime >= WaitAfterTravelTime)
        { 
            SwapGameObjects();
            count = 0;
            travelTime = 0;

            
        }
    }

    Vector3 CurvesHighestPoint(Vector3 point1, Vector3 point2)
    {
        return point1 + (point2 - point1) / 2 +  (-1 * Vector3.forward) * heightOfCurve;
    }

    private void SwapGameObjects()
    {
        GameObject go = GameObject1;
        GameObject1 = GameObject2;
        GameObject2 = go;
    }
}
