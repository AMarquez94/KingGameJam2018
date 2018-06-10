using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseButtonHover : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(delegate { OnMouseDown(); });
    }

    void Update()
    {

    }

    // Update is called once per frame
    public void OnMouseDown()
    {
        int index = transform.GetSiblingIndex();
        Menu_GamePlay._instance.UpdateData(index);
        Debug.Log("Registred data");
    }
}
