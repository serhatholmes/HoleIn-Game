using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
    public PolygonCollider2D hole2DCollider;

    public PolygonCollider2D ground2DCollider;
    Mesh GeneratedMesh;

    private void FixedUpdate() {
        
        transform.hasChanged = false;
        hole2DCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
        MakeHole2D();
    }

    private void MakeHole2D(){
        Vector2[] PointPosition = hole2DCollider.GetPath(0);

        for(int i=0; i<PointPosition.Length; i++){

            PointPosition[i] += (Vector2)hole2DCollider.transform.position;

        }

        ground2DCollider.pathCount = 2;
        ground2DCollider.SetPath(1,PointPosition);
    }

    private void Make3DMeshCollider(){
        GeneratedMesh = ground2DCollider.CreateMesh(true,true);

    }
}
