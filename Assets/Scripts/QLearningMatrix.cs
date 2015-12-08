using UnityEngine;
using System.Collections;

public class QLearningMatrix : MonoBehaviour {

    double[,] matrix;

	// Use this for initialization
	void Start () {
        TerrainData td = gameObject.GetComponent<Terrain>().terrainData;
        matrix = new double[td.heightmapWidth, td.heightmapHeight];
        //transformHeightInNonDesired
	}
	
	
}
