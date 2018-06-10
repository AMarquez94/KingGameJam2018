using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Main : MonoBehaviour {

    public GameObject main_play;
    public GameObject main_exit;
	// Use this for initialization
	void Start ()
    {
        main_play.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("scene_pre1", LoadSceneMode.Single); });
        main_exit.GetComponent<Button>().onClick.AddListener(delegate { Application.Quit(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
