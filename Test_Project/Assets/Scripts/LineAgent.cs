using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAgent : MonoBehaviour {

	public Transform target;
    public Terrain terrain;
    private LineRenderer lineRenderer;
    private UnityEngine.AI.NavMeshPath path;
    private float elapsed = 0.0f;
    public float lineHeight = 0.25f;
    public Material material;
	// Use this for initialization
	void Start () {
        lineRenderer = gameObject.AddComponent<LineRenderer>() as LineRenderer;
        lineRenderer.startWidth = .5f;
        lineRenderer.endWidth = .5f;
        lineRenderer.material = material;
		path = new UnityEngine.AI.NavMeshPath();
        UnityEngine.AI.NavMesh.CalculatePath(transform.position, target.position, UnityEngine.AI.NavMesh.AllAreas, path);
        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);
        elapsed = 0.0f;

        if (terrain != null) {
            
        }

	}
	
	// Update is called once per frame
	void Update () {

        //Calculate path
        UnityEngine.AI.NavMesh.CalculatePath(transform.position, target.position, UnityEngine.AI.NavMesh.AllAreas, path);
        //If there's a terrain
        //Create many points between the first two path corners
        if (terrain != null) {

            for (int i = 1; i < path.corners.Length; i++) {
                Vector3 point1 = path.corners[i - 1];
                Vector3 point2 = path.corners[i];
                float distance = Vector3.Distance(point1, point2);
                Vector3 distVec = point1 - point2;
                distVec.Normalize();
                for (int j = 0; j < distance; j++) {
                    Vector3 newPoint = point1 + (j * distVec);
                    Debug.Log(newPoint.ToString());

                }
                //Debug.Log(distance);

            }

        }
        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);

    }
}
