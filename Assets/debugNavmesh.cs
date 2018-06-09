using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class debugNavmesh : MonoBehaviour {

    public GameObject rock;
    public NavMeshSurface nms;
	void Start () {
        Instantiate(rock);
        nms.BuildNavMesh();
	}

}
