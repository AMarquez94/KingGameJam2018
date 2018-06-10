using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu_GamePlay : MonoBehaviour {

    public GameObject board_win;
    public GameObject board_over;
    public GameObject board_game;

    public GameObject over_play;
    public GameObject over_exit;

    public GameObject win_play;
    public GameObject win_exit;

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
                    board_win.SetActive(false);
                    board_over.SetActive(true);
                    board_game.SetActive(false);
                }
                break;
            case GameState.WIN:
                {
                    board_win.SetActive(true);
                    board_over.SetActive(false);
                    board_game.SetActive(false);
                }
                break;
        }

        currentstate = nextstate;
    }
}
