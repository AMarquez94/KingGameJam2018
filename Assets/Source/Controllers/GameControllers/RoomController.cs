using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomController : MonoBehaviour {

    public bool room_clear;
    public Vector2 grid_pos;
    public int max_obstacles;

    private RoomController left;
    private RoomController right;
    private RoomController top;
    private RoomController bottom;

    public int minEnemies;
    public int maxEnemies;
    public float probabilityMeleeEnemies;
    public float probabilityDistanceEnemies;

    public float probabilityNormalDistanceEnemies;
    public float probabilityHomingDistanceEnemies;
    public float probabilityThreeDistanceEnemies;
    public float probabilityAreaDistanceEnemies;

    public float probabilitySpawnOrNotMutation;
    public int minMutators;
    public int maxMutators;

    private LevelGenerator levelGenerator;
	
    void Start()
    {
        room_clear = false;
    }

	
	// Use this for initialization
	public void BuildRoom () {

        GameObject root_obstacles = transform.GetChild(0).gameObject;

        // Close all the blocked doors, we can pick different objects depending on type
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).transform.gameObject;

            if (child.tag != "Scenario" && !child.activeSelf)
            {
                GameObject obstacle = BuildBlock(child.transform.position + new Vector3(0, -1f, 0));
                obstacle.transform.SetParent(root_obstacles.transform);
            }
        }

        levelGenerator = LevelGenerator.instance.GetComponent<LevelGenerator>();

        if (levelGenerator.GetStartTile() == this.gameObject)
        {
            /* If starting room */

        }
        else if (levelGenerator.GetStartTile().GetComponent<RoomController>().grid_pos == this.grid_pos + new Vector2(-1, 0))
        {
            /* If first room */

        }
        else if (levelGenerator.GetBossTile() == this.gameObject)
        {
            /* If boss room */
            float posx = transform.position.x;
            float posy = transform.position.z + 3f;
            Vector3 newPosition = new Vector3(posx, 1f, posy);

            GameObject instance = Instantiate(Resources.Load("Enemies/FirstPhaseBoss") as GameObject);
            instance.transform.position = newPosition;
            instance.transform.SetParent(root_obstacles.transform);
        }
        else
        {
            /* If normal room */

            // Place the obstacles
            int obs_total = (int)UnityEngine.Random.Range(0, max_obstacles);
            float posx = transform.position.x - 5f;
            float posy = transform.position.z - 4f;

            int j = 0;
            List<Vector2> fetch = new List<Vector2>();
            while (j < obs_total)
            {
                int hpos = (int)UnityEngine.Random.Range(0, 10f);
                int vpos = (int)UnityEngine.Random.Range(0, 8f);
                Vector2 d_pos = new Vector2(hpos, vpos);
                if (fetch.Contains(d_pos))
                    continue;

                GameObject obstacle = BuildObstacle(new Vector3(posx + hpos, 0, posy + vpos));// + new Vector3(0, -3f, 0));
                obstacle.transform.eulerAngles = new Vector3(-90, UnityEngine.Random.Range(0, 360), 0);
                obstacle.transform.SetParent(root_obstacles.transform);
                fetch.Add(d_pos);
                j++;
            }

            // Build the navmesh
            //NavMeshSurface surface = this.transform.GetChild(0).GetChild(0).FindChild("Cube").GetComponent<NavMeshSurface>();
            //if(surface != null)
            //{
            //    surface.BuildNavMesh();
            //}

            #region --Place the enemies--
            int valueNumberOfEnemies = Random.Range(minEnemies, maxEnemies);

            Vector3 tile_pos = this.transform.position;
            Vector2 tile_size = LevelGenerator.instance.tile_size;
            int e_i = 0;
            while (e_i < valueNumberOfEnemies)
            {

                int hpos = (int)UnityEngine.Random.Range(0, 10f);
                int vpos = (int)UnityEngine.Random.Range(0, 8f);
                Vector2 d_pos = new Vector2(hpos, vpos);
                if (fetch.Contains(d_pos))
                    continue;

                float valueTypeOfEnemy = Random.value;
                Vector3 newPosition = new Vector3(posx + hpos, 1f, posy + vpos);
                //new Vector3(Random.Range(tile_pos.x - (tile_size.x / 4), tile_pos.x + (tile_size.x / 4)), -2.2f, Random.Range(tile_pos.z - (tile_size.y / 4), tile_pos.z + (tile_size.y / 4)));
                if (valueTypeOfEnemy < probabilityMeleeEnemies)
                {
                    /* Create a melee enemy */
                    GameObject instance = Instantiate(Resources.Load("Enemies/ChaseEnemy") as GameObject);
                    instance.GetComponent<ChaseEnemyController>().playerPos = GameObject.FindGameObjectWithTag("Player").transform;
                    instance.transform.position = newPosition;
                    instance.transform.SetParent(root_obstacles.transform);
                    instance.SetActive(false);
                    /* TODO: Edit enemy */
                }
                else
                {
                    float valueTypeOfBulletEnemy = Random.value;
                    GameObject instance = Instantiate(Resources.Load("Enemies/MainShootEnemy") as GameObject);
                    instance.transform.position = newPosition;
                    instance.transform.SetParent(root_obstacles.transform);
                    MainShootEnemyController shootEnemyController = instance.GetComponent<MainShootEnemyController>();
                    shootEnemyController.playerPos = GameObject.FindGameObjectWithTag("Player").transform;
                    if (valueTypeOfBulletEnemy < probabilityNormalDistanceEnemies)
                    {
                        /* Create a normal distance enemy */
                        shootEnemyController.bullet = Resources.Load("Bullets/Bullet") as GameObject;
                    }
                    else if (valueTypeOfBulletEnemy < probabilityNormalDistanceEnemies + probabilityHomingDistanceEnemies)
                    {
                        /* Create a homing distance enemy */
                        shootEnemyController.bullet = Resources.Load("Bullets/Bullet_Follow") as GameObject;
                    }
                    else if (valueTypeOfBulletEnemy < probabilityNormalDistanceEnemies + probabilityHomingDistanceEnemies + probabilityThreeDistanceEnemies)
                    {
                        /* Create a homing distance enemy */
                        shootEnemyController.bullet = Resources.Load("Bullets/Bullet_Triple") as GameObject;
                    }
                    else
                    {
                        /* Create an area distance enemy */
                        shootEnemyController.bullet = Resources.Load("Bullets/Bullet_Area") as GameObject;
                    }
                    instance.SetActive(false);
                }

                fetch.Add(d_pos);
                e_i++;
            }
            #endregion --Place the enemies--

            //Place the mutators
            float spawnMutationYesOrNo = Random.value;
            if (spawnMutationYesOrNo >= probabilitySpawnOrNotMutation)
            {
                int numberMutators = Random.Range(minMutators, maxMutators);
                int m_i = 0;
                while (m_i < numberMutators)
                {
                    int hpos = (int)UnityEngine.Random.Range(0, 10f);
                    int vpos = (int)UnityEngine.Random.Range(0, 8f);
                    Vector2 d_pos = new Vector2(hpos, vpos);
                    if (fetch.Contains(d_pos))
                        continue;


                    Vector3 newPosition = new Vector3(posx + hpos, 1f, posy + vpos);
                    GameObject instance = Instantiate(Resources.Load("Mutation") as GameObject);
                    instance.transform.position = newPosition;
                    instance.transform.SetParent(root_obstacles.transform);
                    m_i++;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateAreThereEnemies();
    }

    // Destroy all the enemies and stuff inside this room here
    public void DestroyRoom()
    {

    }

    // Randomly spawn the entities in this room;
    public void InitRoom()
    {
        levelGenerator = GameObject.FindObjectOfType<LevelGenerator>();

        if (levelGenerator.GetStartTile() == this.gameObject)
        {
            /* If starting room */
            print("Starting room");

        }
        else if (levelGenerator.GetStartTile().GetComponent<RoomController>().grid_pos == this.grid_pos + new Vector2(-1, 0))
        {
            /* If first room */
            print("First room");

        }
        else if (levelGenerator.GetBossTile() == this.gameObject)
        {
            /* If boss room */
            print("Boss room");
        }
        else
        {
            /* If normal room */
            print("Normal room");
            enemyController[] enemies = transform.GetChild(0).transform.GetComponentsInChildren<enemyController>(true);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].gameObject.SetActive(true);
            }
        }

        // Close all the blocked doors, we can pick different objects depending on type
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).transform.gameObject;

            if (child.tag != "Scenario" && !child.activeSelf)
                BuildBlock(child.transform.position);
        }
    }

    public GameObject BuildBlock(Vector3 pos)
    {
        string prefab = (int)UnityEngine.Random.Range(0, 1) == 0 ? "Prefabs/Block1" : "Prefabs/Block2";

        // Load a prefab instead.
        GameObject new_block = Instantiate(Resources.Load(prefab, typeof(GameObject))) as GameObject;
        new_block.transform.position = pos;
        new_block.transform.localScale = new Vector3(2, 2, 2);

        return new_block;
    }

    public GameObject BuildObstacle(Vector3 pos)
    {
        // Load a prefab instead.
        //GameObject new_block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject new_block = Instantiate(Resources.Load("Prefabs/Obstacle", typeof(GameObject))) as GameObject;
        new_block.transform.position = pos;
        new_block.transform.localScale = new Vector3(1, 1, 1);

        return new_block;
    }

    public void UpdateAreThereEnemies()
    {
        bool areEnemies = transform.GetComponentsInChildren<enemyController>(true).Length != 0;
        if (!room_clear && !areEnemies)
        {
            /* First frame without enemies - maybe we do something */
            openDoors();
        }
        room_clear = !areEnemies;
    }

    public void openDoors()
    {
        GameObject door;
        door = transform.Find("Tile_TDoor").gameObject;
        door.GetComponent<MeshRenderer>().enabled = false;
        door.GetComponent<BoxCollider>().isTrigger = true;

        door = transform.Find("Tile_BDoor").gameObject;
        door.GetComponent<MeshRenderer>().enabled = false;
        door.GetComponent<BoxCollider>().isTrigger = true;

        door = transform.Find("Tile_RDoor").gameObject;
        door.GetComponent<MeshRenderer>().enabled = false;
        door.GetComponent<BoxCollider>().isTrigger = true;

        door = transform.Find("Tile_LDoor").gameObject;
        door.GetComponent<MeshRenderer>().enabled = false;
        door.GetComponent<BoxCollider>().isTrigger = true;
    }
}
