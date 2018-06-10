using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerManager : MonoBehaviour {

    static GameControllerManager _instance;
    public GameObject player;

    private string[] playerNames = new string[]{
        "Heershi",
        "Kixaix",
        "Ithertos",
        "Sodheitho",
        "Issatu",
        "Prujat",
        "Dhrerya",
        "Adoshtha",
        "Dhrashthinga",
        "Ivyimittat",
        "Szelu",
        "Thota",
        "Cholalla",
        "Sciscizal",
        "Oxili",
        "Khastu",
        "Talas",
        "Epakyas",
        "Vrathayas",
        "Tasikindha"
    };

    private int playerRespawns;

    public void playerDied()
    {
        PlayerController p_controller = player.GetComponent<PlayerController>();
        BulletController p_bulletController = p_controller.bullet.GetComponent<BulletController>();
        DNARegistry newRegistry = new DNARegistry();
        newRegistry.name = p_controller.playerName;
        newRegistry.mutation = p_controller.mutationToString();
        newRegistry.life = p_controller.life;
        newRegistry.maxSpeed = p_controller.maxSpeed;
        newRegistry.cadency = p_controller.cadency;
        newRegistry.damage = p_bulletController.damage;
        newRegistry.bulletSpeed = p_bulletController.speed;
        newRegistry.bulletrange = p_bulletController.range;

        registry.Add(newRegistry);

        playerRespawns++;

        Menu_GamePlay._instance.ChangeState(Menu_GamePlay.GameState.OVER);
    }

    public void EnemyDied(string name)
    {
        if(name == "FirstPhaseBoss(Clone)")
        {
            PlayerController p_controller = player.GetComponent<PlayerController>();
            BulletController p_bulletController = p_controller.bullet.GetComponent<BulletController>();
            DNARegistry newRegistry = new DNARegistry();
            newRegistry.name = p_controller.playerName;
            newRegistry.mutation = p_controller.mutationToString();
            newRegistry.life = p_controller.life;
            newRegistry.maxSpeed = p_controller.maxSpeed;
            newRegistry.cadency = p_controller.cadency;
            newRegistry.damage = p_bulletController.damage;
            newRegistry.bulletSpeed = p_bulletController.speed;
            newRegistry.bulletrange = p_bulletController.range;

            registry.Add(newRegistry);
            /* End game */

        }
    }

    public int getPlayerRespawns()
    {
        return playerRespawns;
    }

    public string getRandomPlayerName()
    {
        int index = Random.Range(0, playerNames.Length-1);
        return playerNames[index];
    }

    public class DNARegistry
    {
        public string name;
        public string mutation;
        public int life;
        public float maxSpeed;
        public float cadency;
        public float damage;
        public float bulletSpeed;
        public float bulletrange;
    }

    public List<DNARegistry> registry;

    public bool fading;
    const float INCREMENT = 0.01f;
    const float MAX_BLEND = 2;

    void Awake () {
        _instance = this;
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        Object.DontDestroyOnLoad(this.gameObject);
        playerRespawns = 0;
        registry = new List<DNARegistry>();
	}
	
    public static GameControllerManager getGameControllerManager()
    {
        if(_instance == null)
        {
            GameObject new_inst = new GameObject("GameControllerManager");
            _instance = new_inst.AddComponent<GameControllerManager>();
        }
        return _instance;
    }

    public GameObject getPlayer()
    {
        return _instance.player;
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            if(player.GetComponent<PlayerController>().myState == PlayerController.States.DEAD)
            {
                /* Gestionar el reinicio */
                playerDied();
                /* Y cuando este todo -> reiniciar escena */

            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            playerDied();
            //SceneManager.LoadScene("scene_pre1", LoadSceneMode.Single);
        }
        //if (fading)
        //{
        //    fadeValue += INCREMENT;
        //    so.intensity = fadeValue * MAX_BLEND;

        //    if (fadeValue >= 1)
        //    {
        //        fading = false;
        //        // change player position
        //    }
        //}
        //else if (fadeValue > 0)
        //{
        //    fadeValue = Mathf.Max(0, fadeValue - INCREMENT);
        //    so.intensity = fadeValue * MAX_BLEND;
        //}
    }
}
