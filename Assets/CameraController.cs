using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Material material;
    public bool isBlackAndWhite;
    public bool isBlurred;

	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {
        material = new Material(Shader.Find("Hidden/BWtransition"));
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (isBlackAndWhite)
        {
            material.SetFloat("_bwBlend", 1);
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
