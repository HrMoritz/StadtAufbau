using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public static WorldGeneration instance;

    private void Awake()
    {
        instance = this;
        GenerateBasicWorld();
    }

    public int sizeX;
    public int sizeY;
    public float tileSize;
    public GameObject tilePrefab;
    PathNode[,] grid;
    private void Start()
    {
    }

    private void GenerateBasicWorld()
    {
        float maxDistanceX = -((sizeX / 2f) - 0.5f) * tileSize;
        float maxDistanceY = -((sizeY / 2f) - 0.5f) * tileSize;

        grid = new PathNode[sizeX, sizeY];
        Vector3 position = new Vector3(0, 0, maxDistanceY);
        for (int i = 0; i < sizeY; i++)
        {
            position.x = maxDistanceX;
            for (int j = 0; j < sizeX; j++)
            {
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                PathNode node = tile.GetComponent<PathNode>();
                grid[j, i] = node;
                node.gridX = j;
                node.gridY = i;
                position.x += tileSize;
            }
            position.z += tileSize;
        }
    }

    public int MaxSize
    {
        get
        {
            return sizeX * sizeY;
        }
    }

    public List<PathNode> GetNeighbours(PathNode node)
    {
        List<PathNode> neighbours = new List<PathNode>();

        for(int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if ((x == 0 && y == 0) || (x != 0 && y != 0))
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < sizeX && checkY >= 0 && checkY < sizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    public PathNode NodeFromWorldPoint(Vector3 worldPosition)
    {
        float maxDistanceX = ((sizeX / 2f) - 0.5f) * tileSize;
        float maxDistanceY = ((sizeY / 2f) - 0.5f) * tileSize;
        float percentX = (worldPosition.x + maxDistanceX) / (maxDistanceX + maxDistanceX);
        float percentY = (worldPosition.z + maxDistanceY) / (maxDistanceY + maxDistanceY);
        //not really working
        //float percentX = (worldPosition.x + (sizeX * tileSize) / 2) / (sizeX * tileSize);
        //float percentY = (worldPosition.z + (sizeY * tileSize) / 2) / (sizeY * tileSize);
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((sizeX * tileSize - 1) * percentX);
        int y = Mathf.RoundToInt((sizeY * tileSize - 1) * percentY);
        return grid[x, y];
    }
}
