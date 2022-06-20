using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColors : MonoBehaviour
{
    public Color normalColor;
    public Color blockedColor;
    public Color goalColor;
    public Color playerColor;
    public Color highlightColor;

    public bool highlight;
    public bool goal;
    public bool player;

    private void Update()
    {
        
        if (player)
        {
            SetColorPlayer();
        }
        else if (goal)
        {
            SetColorGoal();
        }
        else if (highlight)
        {
            SetColorHighlight();
        }
        else if (!GetComponent<PathNode>().walkable)
        {
            SetColorBlocked();
        }
        else
        {
            SetColorNormal();
        }
    }

    public void SetColorNormal()
    {
        SetColor(normalColor);
    }
    public void SetColorBlocked()
    {
        SetColor(blockedColor);
    }
    public void SetColorGoal()
    {
        SetColor(goalColor);
    }
    public void SetColorPlayer()
    {
        SetColor(playerColor);
    }
    public void SetColorHighlight()
    {
        SetColor(highlightColor);
    }

    private void SetColor(Color c)
    {
        var renderer = GetComponent<Renderer>();

        renderer.material.SetColor("_Inner", c);
    }
}
