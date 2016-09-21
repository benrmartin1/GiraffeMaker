using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class RandomNamer : MonoBehaviour
{
    private List<System.String> names;
    int x = 0;
    private string startingList = "Names.txt";

    void Awake()
    {
        x = UnityEngine.Random.Range(0, 100000);
        StreamReader sr = File.OpenText("Assets/Data/" + startingList);
        names = new List<System.String>(sr.ReadToEnd().Split("\n"[0]));
        for(int i = 0; i < names.Count; i++)
        {
            // remove whitespace
            names[i] = names[i].Trim();
        }
        sr.Close();
    }

    public void SwitchList(string newList)
    {
        x = UnityEngine.Random.Range(0, 100000);
        StreamReader sr = File.OpenText("Assets/Data/" + newList);
        names = new List<System.String>(sr.ReadToEnd().Split("\n"[0]));
        for (int i = 0; i < names.Count; i++)
        {
            // remove whitespace
            names[i] = names[i].Trim();
        }
        sr.Close();
    }

    public string RandomName()
    {
        Random.InitState(x++);
        string name = names[Random.Range(0, names.Count)];
        return name;
    }
}