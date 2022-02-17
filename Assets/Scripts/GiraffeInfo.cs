using UnityEngine;
using System.Collections;
using System;

// Basic info about giraffe
public class GiraffeInfo : MonoBehaviour
{
    private float height;
    private float width;

    public void SetHeight(float height)
    {
        this.height = height;
    }

    public float GetHeight()
    {
        return height;
    }

    public void SetWidth(float width)
    {
        this.width = width;
    }

    public float GetWidth()
    {
        return width;
    }
}
