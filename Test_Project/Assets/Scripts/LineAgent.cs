using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAgent : MonoBehaviour {

	public Transform target;
    private LineRenderer lineRenderer;
    private UnityEngine.AI.NavMeshPath path;
    private float elapsed = 0.0f;
	// Use this for initialization
	void Start () {
        lineRenderer = gameObject.AddComponent<LineRenderer>() as LineRenderer;
        lineRenderer.startWidth = .5f;
        lineRenderer.endWidth = .5f;
		path = new UnityEngine.AI.NavMeshPath();
        UnityEngine.AI.NavMesh.CalculatePath(transform.position, target.position, UnityEngine.AI.NavMesh.AllAreas, path);
        lineRenderer.SetVertexCount(path.corners.Length);
        lineRenderer.SetPositions(path.corners);
        elapsed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		// Update the way to the goal every second.
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
        }
            UnityEngine.AI.NavMesh.CalculatePath(transform.position, target.position, UnityEngine.AI.NavMesh.AllAreas, path);
             lineRenderer.SetVertexCount(path.corners.Length);

        lineRenderer.SetPositions(path.corners);

        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);

    }
}
