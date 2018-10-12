using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAgent : MonoBehaviour {

	public Transform target;
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
	}
	
	// Update is called once per frame
	void Update () {
		// Update the way to the goal every second.
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
        }

        lineRenderer.material = material;


        Vector3[] vectors = new Vector3[path.corners.Length];

        int j = 0;
        foreach (Vector3 v in path.corners)
        {
            vectors[j] = v;
            vectors[j].y = vectors[j].y + lineHeight;
            j++;
        }

        UnityEngine.AI.NavMesh.CalculatePath(transform.position, new Vector3(target.position.x, 0, target.position.z), UnityEngine.AI.NavMesh.AllAreas, path);
        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(vectors);

        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);

    }
}
