using UnityEngine;
using System.Collections;

public class QLearningMask  {

    private bool[,] data;

    public QLearningMask() {
        TerrainData td = GameObject.FindObjectOfType<Terrain>().terrainData;
        data = new bool[td.heightmapWidth, td.heightmapHeight];
    }

    public void unlockPosition(int x, int y){
        data[x, y] = true;
    }

    public bool[,] getData(){
        return data;
    }

    public void merge(QLearningMask mask) {
        bool[,] friendMask = mask.getData();

        for (int i = 0; i < data.GetLength(0); i++) {
            for (int j = 0; j < data.GetLength(1); j++) {
                data[i, j] = data[i, j] || friendMask[i, j];
            }
        }
    }

}
