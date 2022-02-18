using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public Mesh[] meshes;

	// Use this for initialization
	void Start () 
    {
        Mesh mesh = meshes[Random.Range(0, meshes.Length)];
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
	}
}
