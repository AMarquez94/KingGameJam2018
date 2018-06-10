using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Main : MonoBehaviour {

    public GameObject main_play;
    public GameObject main_exit;
    public GameObject main_instructions;
    public GameObject instr_back;

    public GameObject main_panel;
    public GameObject inst_panel;
   
	// Use this for initialization
	void Start ()
    {
        inst_panel.SetActive(false);
        main_play.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("scene_pre1", LoadSceneMode.Single); });
        main_exit.GetComponent<Button>().onClick.AddListener(delegate { Application.Quit(); });
        main_instructions.GetComponent<Button>().onClick.AddListener(delegate { main_panel.SetActive(false); inst_panel.SetActive(true); });
        instr_back.GetComponent<Button>().onClick.AddListener(delegate { main_panel.SetActive(true); inst_panel.SetActive(false); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
