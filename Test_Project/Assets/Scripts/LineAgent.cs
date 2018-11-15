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
	}
	
	// Update is called once per frame
	void Update () {

        //Calculate path
        Vector3 playerPos = new Vector3();
        playerPos.y = target.position.y;
        playerPos.x = target.position.x;
        playerPos.z = target.position.z;
        if (terrain != null)
        {
            playerPos.y = terrain.SampleHeight(target.position);
            UnityEngine.AI.NavMesh.CalculatePath(transform.position, playerPos, UnityEngine.AI.NavMesh.AllAreas, path);
        }
        else
        {
            //checks 50 units up and down to see if it connect to path
            UnityEngine.AI.NavMesh.CalculatePath(transform.position, playerPos, UnityEngine.AI.NavMesh.AllAreas, path);
            int attempts = 0;
            float originalY = playerPos.y;
            while(path.corners.Length == 0 && attempts < 50)
            {
                playerPos.y += 1;
                UnityEngine.AI.NavMesh.CalculatePath(transform.position, playerPos, UnityEngine.AI.NavMesh.AllAreas, path);
                attempts++;
            }
            playerPos.y = originalY;
            attempts = 0;
            while (path.corners.Length == 0 && attempts < 50)
            {
                playerPos.y -= 1;
                UnityEngine.AI.NavMesh.CalculatePath(transform.position, playerPos, UnityEngine.AI.NavMesh.AllAreas, path);
                attempts++;
            }

        }
       

        //If there's a terrain
        //Create many points between the first two path corners
        if (terrain != null) {

            int totalPoints = 0;
            for (int i = 1; i < path.corners.Length; i++) {

                Vector3 point1 = path.corners[i - 1];
                Vector3 point2 = path.corners[i];
                float distance = Vector3.Distance(point1, point2);
        
                for (int j = 0; j < (int)distance*5; j++) {
                    totalPoints++;
                    Vector3 newPoint = Vector3.Lerp(point1, point2, (float)j / distance);
                    float height = terrain.SampleHeight(newPoint);
                    newPoint.y = height + lineHeight;
                    lineRenderer.positionCount = totalPoints;
                    lineRenderer.SetPosition(totalPoints - 1, newPoint);
                }
            }
        } else {
            lineRenderer.positionCount = path.corners.Length;
            for (int j = 0; j < path.corners.Length; j++) {
                Vector3 newPoint = path.corners[j];
                newPoint.y = newPoint.y + lineHeight;
                lineRenderer.SetPosition(j, newPoint);
            }
        }
    }
}
