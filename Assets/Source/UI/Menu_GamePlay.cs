using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu_GamePlay : MonoBehaviour {

    public static Menu_GamePlay _instance;

    public GameObject board_win;
    public GameObject board_over;
    public GameObject board_game;

    public GameObject over_play;
    public GameObject over_exit;

    public GameObject win_play;
    public GameObject win_exit;

    public GameObject win_but;
    public GameObject def_but;

    public GameObject win_text;
    public GameObject def_text;

    public enum GameState
    {
        GAME,
        WIN,
        OVER
    };

    public GameState currentstate;

	// Use this for initialization
	void Start ()
    {
        _instance = this;
        over_play.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("scene_pre1", LoadSceneMode.Single); });
        over_exit.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("scene_main", LoadSceneMode.Single); });
        win_play.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("scene_pre1", LoadSceneMode.Single); });
        win_exit.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("scene_main", LoadSceneMode.Single); });
        ChangeState(GameState.GAME);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void StatedFadeIn()
    {

    }

    public void StateFadeOut()
    {

    }

    public void ChangeState(GameState nextstate)
    {
        switch (nextstate)
        {
            case GameState.GAME:
                {
                    board_win.SetActive(false);
                    board_over.SetActive(false);
                    board_game.SetActive(true);
                }
                break;
            case GameState.OVER:
                {
                    GameControllerManager man = GameControllerManager.getGameControllerManager();

                    {
                        for (int i = 0; i < win_but.transform.childCount; i++)
                            win_but.transform.GetChild(i).gameObject.SetActive(true);

                        for (int i = 0; i < def_but.transform.childCount; i++)
                            def_but.transform.GetChild(i).gameObject.SetActive(true);
                    }

                    {
                        for (int i = man.registry.Count; i < win_but.transform.childCount; i++) {
                            win_but.transform.GetChild(i).gameObject.SetActive(false);
                            Debug.Log("set to false");
                        }

                        for (int i = man.registry.Count; i < def_but.transform.childCount; i++)
                            def_but.transform.GetChild(i).gameObject.SetActive(false);
                    }

                    board_win.SetActive(false);
                    board_over.SetActive(true);
                    board_game.SetActive(false);
                }
                break;
            case GameState.WIN:
                {
                    GameControllerManager man = GameControllerManager.getGameControllerManager();

                    {
                        for (int i = 0; i < win_but.transform.childCount; i++)
                            win_but.transform.GetChild(i).gameObject.SetActive(true);

                        for (int i = 0; i < def_but.transform.childCount; i++)
                            def_but.transform.GetChild(i).gameObject.SetActive(true);
                    }

                    {
                        for (int i = man.registry.Count; i < win_but.transform.childCount; i++)
                            win_but.transform.GetChild(i).gameObject.SetActive(false);

                        for (int i = man.registry.Count; i < def_but.transform.childCount; i++)
                            def_but.transform.GetChild(i).gameObject.SetActive(false);
                    }

                    board_win.SetActive(true);
                    board_over.SetActive(false);
                    board_game.SetActive(false);
                }
                break;
        }

        currentstate = nextstate;
    }

    public void UpdateData(int index)
    {
        GameControllerManager man = GameControllerManager.getGameControllerManager();
        GameControllerManager.DNARegistry reg = man.registry[index];

        win_text.transform.GetChild(0).transform.GetComponent<Text>().text = reg.name;
        win_text.transform.GetChild(1).transform.GetComponent<Text>().text = reg.mutation;
        win_text.transform.GetChild(2).transform.GetComponent<Text>().text = reg.life.ToString("0.0");
        win_text.transform.GetChild(3).transform.GetComponent<Text>().text = reg.maxSpeed.ToString("0.0");
        win_text.transform.GetChild(4).transform.GetComponent<Text>().text = reg.cadency.ToString("0.0");
        win_text.transform.GetChild(5).transform.GetComponent<Text>().text = reg.damage.ToString("0.0");
        win_text.transform.GetChild(6).transform.GetComponent<Text>().text = reg.bulletSpeed.ToString("0.0");
        win_text.transform.GetChild(7).transform.GetComponent<Text>().text = reg.bulletrange.ToString("0.0");

        def_text.transform.GetChild(0).transform.GetComponent<Text>().text = reg.name;
        def_text.transform.GetChild(1).transform.GetComponent<Text>().text = reg.mutation;
        def_text.transform.GetChild(2).transform.GetComponent<Text>().text = reg.life.ToString("0.0");
        def_text.transform.GetChild(3).transform.GetComponent<Text>().text = reg.maxSpeed.ToString("0.0");
        def_text.transform.GetChild(4).transform.GetComponent<Text>().text = reg.cadency.ToString("0.0");
        def_text.transform.GetChild(5).transform.GetComponent<Text>().text = reg.damage.ToString("0.0");
        def_text.transform.GetChild(6).transform.GetComponent<Text>().text = reg.bulletSpeed.ToString("0.0");
        def_text.transform.GetChild(7).transform.GetComponent<Text>().text = reg.bulletrange.ToString("0.0");
    }
}
