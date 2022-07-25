using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnChangePosition : MonoBehaviour
{
    public PolygonCollider2D hole2DCollider;

    public PolygonCollider2D ground2DCollider;

    public MeshCollider GenerateMeshCollider;
    public Collider GroundCollider;
    public float initialScale = 0.5f;
    Mesh GeneratedMesh;

    public void Move(BaseEventData mEvent){
        if(((PointerEventData)mEvent).pointerCurrentRaycast.isValid){
            transform.position = ((PointerEventData)mEvent).pointerCurrentRaycast.worldPosition;
        }
    }

    public IEnumerator ScaleHole(){
        Vector3 StartScale = transform.localScale;
        Vector3 EndScale = StartScale *2;

        float t = 0;
        while( t <= 0.4f){
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(StartScale,EndScale,t);
            yield return null;
        }


    }

    private void Start() {
        GameObject[] AllGameObj = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach(var go in AllGameObj ){
            if(go.layer == LayerMask.NameToLayer("Obstacle")){
                Physics.IgnoreCollision(go.GetComponent<Collider>(),GenerateMeshCollider,true);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        Physics.IgnoreCollision(other,GroundCollider,true);
        Physics.IgnoreCollision(other,GenerateMeshCollider,false);
    }

    private void OnTriggerExit(Collider other) {
        Physics.IgnoreCollision(other,GroundCollider,false);
        Physics.IgnoreCollision(other,GenerateMeshCollider,true);
    }
    private void FixedUpdate() {
        
        transform.hasChanged = false;
        hole2DCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
        hole2DCollider.transform.localScale = transform.localScale * initialScale;
        MakeHole2D();
        Make3DMeshCollider();
    }

    private void MakeHole2D(){
        Vector2[] PointPosition = hole2DCollider.GetPath(0);

        for(int i=0; i<PointPosition.Length; i++){

            PointPosition[i]  = hole2DCollider.transform.TransformPoint(PointPosition[i]);

        }

        ground2DCollider.pathCount = 2;
        ground2DCollider.SetPath(1,PointPosition);
    }

    private void Make3DMeshCollider(){

        if(GeneratedMesh != null) {Destroy(GeneratedMesh);}
        GeneratedMesh = ground2DCollider.CreateMesh(true,true);
        GenerateMeshCollider.sharedMesh = GeneratedMesh; 
    }
}
