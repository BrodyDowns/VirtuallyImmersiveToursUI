using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Source https://www.youtube.com/watch?v=vFvwyu_ZKfU
public class GenTerrain : MonoBehaviour {

	public int depth = 20; //y
	public int width = 256; //x
	public int height = 256; //z
	public float scale = 20f;

	// Use this for initialization
	void Start () {
		Terrain terrain = GetComponent<Terrain>();
		terrain.terrainData = GenerateTerrain(terrain.terrainData);
	}

	TerrainData GenerateTerrain(TerrainData terrainData) {
		terrainData.heightmapResolution = width + 1;
		terrainData.size = new Vector3(width, depth, height);
		terrainData.SetHeights(0, 0, GenerateHeights());
		return terrainData;
	}

	float[,] GenerateHeights(){
		float[,] heights = new float[width, height];
		int [,] path = GeneratePath();
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (path[x, y] != 1) {
					heights[x, y] = CalculateHeight(x, y);
				} else {
					heights[x, y] = 0;
				}
				
			}
		}

		return heights;
	}

	int[,] GeneratePath() {

		int[,] path = new int[width, height];
        System.Random rand = new System.Random();
		Boolean done = false;

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				path[i, j] = 0;
			}
		}

		int x = 0;
		int y = 0;
		while (!done) {
			int picker = rand.Next(0, 2);

			if (picker == 1 && x < width - 1) {
				path[x, y] = 1;

				if(y > 0) {
					path[x, y - 1] = 1;
				}

				if(y < height - 1) {
					path[x, y + 1] = 1;
				}

				if(x + 1 < width - 1){
					path[x + 1, y] = 1;
				}

				if (x + 1 < width -1 && y > 0) {
					path[x + 1, y - 1] = 1;
				}

				if(x + 1 < width - 1 && y + 1 < height - 1){
					path[x + 1, y + 1] = 1;
				}

				x++;

			} else if (picker == 0 && y < height - 1) {
				path[x, y] = 1;

				if (x > 0) {
					path[x - 1, y] = 1;
				}

				if (x < width - 1) {
					path[x + 1, y] = 1; 
				}

				if (y < height - 1) {
					path[x, y + 1] = 1;
				}

				if(x > 0 && y + 1 < height - 1){
					path[x - 1, y + 1] = 1;
				}

				if(x + 1 < width - 1 && y + 1 < height - 1) {
					path[x + 1, y + 1] = 1;
				}

				y++;
			}

			if (x == width - 1 && y == height - 1) {
				done = true;
			}
		}

		return path;
	}

	float CalculateHeight(int x, int y) {
		float xCoord = (float)x / width * scale;
		float yCoord = (float)y / height * scale;

		return Mathf.PerlinNoise(xCoord, yCoord);
	}
}
