using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private LevelGenerator levelGenerator;

	// Use this for initialization
	void Start () {

        room_clear = true;
    }
	
	// Update is called once per frame
	void Update () {

	}

    // Destroy all the enemies and stuff inside this room here
    public void DestroyRoom()
    {

    }

    public void BuildRoom()
    {
        levelGenerator = GameObject.FindObjectOfType<LevelGenerator>();

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
        }
        else
        {
            /* If normal room */

            // Place the obstacles
            int index = 0;
            for (int i = 0; i < max_obstacles; i++)
            {

            }

            // Build the navmesh


            // Place the enemies
            int valueNumberOfEnemies = Random.Range(minEnemies, maxEnemies);

            Vector3 tile_pos = this.transform.position;
            Vector2 tile_size = LevelGenerator.instance.tile_size;
            for (int i = 0; i < valueNumberOfEnemies; i++)
            {
                float valueTypeOfEnemy = Random.value;
                GameObject candidate = transform.GetChild(0).gameObject;
                Vector3 newPosition = new Vector3(Random.Range(tile_pos.x - (tile_size.x / 4), tile_pos.x + (tile_size.x / 4)), -2.2f, Random.Range(tile_pos.z - (tile_size.y / 4), tile_pos.z + (tile_size.y / 4)));
                if (valueTypeOfEnemy < probabilityMeleeEnemies)
                {
                    /* Create a melee enemy */
                    GameObject instance = Instantiate(Resources.Load("Enemies/ChaseEnemy") as GameObject);
                    instance.GetComponent<ChaseEnemyController>().playerPos = GameObject.FindGameObjectWithTag("Player").transform;
                    instance.transform.position = newPosition;
                    instance.transform.SetParent(candidate.transform);
                    instance.SetActive(false);
                    /* TODO: Edit enemy */
                }
                else
                {
                    float valueTypeOfBulletEnemy = Random.value;
                    GameObject instance = Instantiate(Resources.Load("Enemies/MainShootEnemy") as GameObject);
                    instance.transform.position = newPosition;
                    instance.transform.SetParent(candidate.transform);
                    MainShootEnemyController shootEnemyController = instance.GetComponent<MainShootEnemyController>();
                    shootEnemyController.playerPos = GameObject.FindGameObjectWithTag("Player").transform;
                    if (valueTypeOfBulletEnemy < probabilityNormalDistanceEnemies)
                    {
                        /* Create a normal distance enemy */
                        shootEnemyController.bullet = Resources.Load("Bullets/Bullet") as GameObject;
                    }
                    else if (valueTypeOfBulletEnemy >= probabilityNormalDistanceEnemies && valueTypeOfBulletEnemy < probabilityNormalDistanceEnemies + probabilityHomingDistanceEnemies)
                    {
                        /* Create a homing distance enemy */
                        shootEnemyController.bullet = Resources.Load("Bullets/Bullet_Follow") as GameObject;
                    }
                    else if (valueTypeOfBulletEnemy >= probabilityNormalDistanceEnemies + probabilityHomingDistanceEnemies && valueTypeOfBulletEnemy < probabilityNormalDistanceEnemies + probabilityHomingDistanceEnemies + probabilityThreeDistanceEnemies)
                    {
                        /* Create a homing distance enemy */
                        shootEnemyController.bullet = Resources.Load("Bullets/Bullet_Triple") as GameObject;
                    }
                    else
                    {
                        /* Create an area distance enemy */
                        shootEnemyController.bullet = Resources.Load("Bullets/Bullet") as GameObject;
                    }
                    instance.SetActive(false);
                }
            }
        }
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

    public void BuildBlock(Vector3 pos)
    {
        // Load a prefab instead.
        GameObject new_block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        new_block.transform.position = pos;
        new_block.transform.localScale = new Vector3(2, 2, 2);
    }

    public void BuildObstacle(Vector3 pos)
    {
        // Load a prefab instead.
        GameObject new_block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        new_block.transform.position = pos;
        new_block.transform.localScale = new Vector3(1, 1, 1);
    }

    public void UpdateAreThereEnemies()
    {
        bool areEnemies = transform.GetComponentsInChildren<enemyController>().Length != 0;
        if (!room_clear && !areEnemies)
        {
            /* First frame without enemies - maybe we do something */

        }
        room_clear = !areEnemies;
    }
}
