using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    PathNode start;
    PathNode end;

    PathAgent selected;

    List<PathNode> path;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (selected)
            {
                if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Tile")))
                {
                    PathRequestManager.RequestPath(selected.transform.position, hit.point, selected.OnPathFound);
                    selected = null;
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Agent")))
                {
                    selected = hit.collider.GetComponent<PathAgent>();
                }
            }
        }
    }
}
