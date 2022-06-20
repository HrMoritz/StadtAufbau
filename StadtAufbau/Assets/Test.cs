using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    PathNode start;
    PathNode end;

    List<PathNode> path;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Tile")))
            {
                if (start)
                {
                    start.GetComponent<TileColors>().player = false;
                }
                start = WorldGeneration.instance.NodeFromWorldPoint(hit.point);
                start.GetComponent<TileColors>().player = true;
                SetPath();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Tile")))
            {
                if (end)
                {
                    end.GetComponent<TileColors>().goal = false;
                }
                end = WorldGeneration.instance.NodeFromWorldPoint(hit.point);
                end.GetComponent<TileColors>().goal = true;
                SetPath();
            }
        }

        else if (Input.GetMouseButtonDown(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Tile")))
            {
                WorldGeneration.instance.NodeFromWorldPoint(hit.point).walkable = !WorldGeneration.instance.NodeFromWorldPoint(hit.point).walkable;
                SetPath();
            }
        }
    }

    private void SetPath()
    {
        if (start != null && end != null)
        {
            if (path != null)
            {
                foreach (PathNode node in path)
                {
                    node.GetComponent<TileColors>().highlight = false;
                }
            }
           // path = Pathfinding.instance.FindPath(start.transform.position, end.transform.position);
            foreach (PathNode node in path)
            {
                node.GetComponent<TileColors>().highlight = true;
            }
        }
    }
}
