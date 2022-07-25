using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditions : MonoBehaviour
{
    public int Point = 0;
    public OnChangePosition HoleScript;
    private void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
        CalculateProgress();
    }

    private void CalculateProgress(){
        Point++;

        if(Point % 10 == 0){
            StartCoroutine(HoleScript.ScaleHole());
        }
    }
}
