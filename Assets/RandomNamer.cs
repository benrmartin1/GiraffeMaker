using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class RandomNamer : MonoBehaviour
{
    private List<String> names;
    int x = 0;
    void Start()
    {
        x = UnityEngine.Random.Range(0, 100000);
        StreamReader sr = File.OpenText("Assets/Data/Names.txt");
        names = new List<String>(sr.ReadToEnd().Split("\n"[0]));
        sr.Close();
    }


    public string RandomName()
    {
        UnityEngine.Random.InitState(x++);
        return names[UnityEngine.Random.Range(0, names.Count)];
    }
}