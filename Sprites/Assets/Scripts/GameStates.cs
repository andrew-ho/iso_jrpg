using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour {

    Rigidbody rb;
    GameObject player;

    public GameState state;
    public GameObject BattleManager;
    GameObject bm;

    private bool created = false;

    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public GameObject List;

    GameObject check;
    Camera cam;
    TestingGrid board;

    RaycastHit hit;
    Ray ray;
    List<GameObject> checkObjects = new List<GameObject>();

    public enum GameState
    {
        WORLDSTATE,
        BATTLESTATE,
        MENUSTATE,
        CHATSTATE
    }

    void randomEncounter(Rigidbody player)
    {
        if (state == GameState.WORLDSTATE)
        {
            if (player.velocity.magnitude > 0)
            {
                if (Random.Range(0, 1000) < 10)
                {
                    Debug.Log("Attacked!");
                    state = GameState.BATTLESTATE;
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        List = GameObject.Find("ActionList");
        player = GameObject.FindGameObjectWithTag("Player");
        bm = GameObject.Find("BoardManager");
        board = bm.GetComponent<TestingGrid>();
        check = GameObject.Find("Check");
        rb = player.GetComponent<Rigidbody>();
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        List.SetActive(false);
        
	}
	
    checkResult getPosition()
    {
        
        //Vector3 v = Camera.main.WorldToScreenPoint(transform.position);
        //Debug.DrawLine(cam.transform.position, player.transform.position + new Vector3(0,-.5f,0), Color.green);
        // create a ray going into the scene from the screen location the user clicked at
        Ray ray = new Ray(check.transform.position, Vector3.down);
        // the raycast hit info will be filled by the Physics.Raycast() call further
        RaycastHit hit;
        LayerMask lay = 1 << 9;
        // perform a raycast using our new ray. 
        // If the ray collides with something solid in the scene, the "hit" structure will
        // be filled with collision information
        //Debug.DrawRay(check.transform.position, Vector3.down, Color.green);
        //if (Physics.Linecast(cam.transform.position, player.transform.position + new Vector3(0, -.5f, 0), out hit, lay))
        //{
            //Debug.Log(hit.collider.name);
        //}
        //Debug.DrawRay(cam.transform.position, player.transform.position, Color.green);
        if (Physics.Raycast(ray, out hit))
        {
            // a collision occured. Check if it's our plane object and create our cube at the
            // collision point, facing toward the collision normal
            /*if (hit.collider == yourPlaneCollider)
                Instantiate(yourCubePrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Debug.Log(hit.collider.name);
            Debug.DrawLine(transform.position, hit.point, Color.red);
            fixRotation fx = new fixRotation();
            fx.OnTriggerEnter(hit.collider);*/
            //Debug.DrawLine(cam.transform.forward + new Vector3(0, 3, -3), hit.point, Color.red);
            //Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.green);
            //Debug.Log(hit.collider.name);
            Debug.DrawRay(check.transform.position, hit.point, Color.red, 500);
            //Debug.Log(hit.collider.name);
            GameObject spawnPoint = hit.collider.gameObject;
            
            return checkSpawn(spawnPoint);
        }
        return null;
        //Vector3 v = fx.OnTriggerEnter(a);
        //Debug.Log(a.name);
        
    }

    public class checkResult {
        public int row;
        public int columns;
        public checkResult(int row, int columns)
        {
            this.row = row;
            this.columns = columns;
        }
    }

    checkResult checkSpawn(GameObject spawn)
    {
        for (int i = 0; i < board.rows; i++)
        {
            for (int k = 0; k < board.columns; k++)
            {
                GameObject spawner = board.tileGrid[i, k];
                if (spawner.name.Equals(spawn.name))
                {
                    //Debug.Log("spawn " + spawn.name);
                    //Debug.Log(i + " " + k);
                    return new checkResult(i, k);
                }
            }
        }
        return null;
    }
	// Update is called once per frame
	void FixedUpdate () {
        randomEncounter(rb);
        Debug.DrawRay(check.transform.position, Vector3.down);
        switch (state)
        {
            case (GameState.BATTLESTATE):
                if (!created)
                {
                    GameObject bm = Instantiate(Resources.Load("Prefabs/BattleManager"), Vector3.zero, Quaternion.identity) as GameObject;
                    getPosition();
                    
                    created = true;
                    checkResult cr = getPosition();
                    bool loc = false;
                    checkObjects.Clear();
                    //Debug.Log(board.tileGrid.GetLength(0) + " " + board.tileGrid.GetLength(1));
                    //Debug.Log("cr.row: " + cr.row + "cr.columns: " + cr.columns + "[i]: " + board.tileGrid.GetLength(0) + "[k]: " + board.tileGrid.GetLength(1));
                    //Debug.Log(board.tileGrid.GetLength(0) + "[k]: " + board.tileGrid.GetLength(1));
                    //Debug.Log(cr.row + " columns: " + cr.columns);
                    if (cr.row + 2 < board.tileGrid.GetLength(0) && cr.columns < board.tileGrid.GetLength(1))
                    {
                        //Debug.Log("the gameobject is:" + board.tileGrid[cr.row + 2, cr.columns]);
                        checkObjects.Add(board.tileGrid[cr.row + 2, cr.columns]);
                    }
                    if (cr.row - 2 >= 0 && cr.columns < board.tileGrid.GetLength(1))
                    {
                        //Debug.Log("the gameobject is:" + board.tileGrid[cr.row - 2, cr.columns]);
                        checkObjects.Add(board.tileGrid[cr.row - 2, cr.columns]);
                    }
                    if (cr.row < board.tileGrid.GetLength(0) && cr.columns + 2 < board.tileGrid.GetLength(1))
                    {
                       // Debug.Log("the gameobject is:" + board.tileGrid[cr.row, cr.columns + 2]);
                        checkObjects.Add(board.tileGrid[cr.row, cr.columns + 2]);
                    }
                    if (cr.row < board.tileGrid.GetLength(0) && cr.columns - 2 >= 0)
                    {
                       // Debug.Log("the gameobject is:" + board.tileGrid[cr.row, cr.columns - 2]);
                        checkObjects.Add(board.tileGrid[cr.row, cr.columns - 2]);
                    }
                    for (int i = 0; i < checkObjects.Count; i++)
                    {
                        if (Physics.Raycast(cam.transform.position, checkObjects[i].transform.position, out hit))
                        {
                            //Debug.DrawRay(cam.transform.position, checkObjects[i].transform.position, Color.blue, 500);
                            //Debug.Log(hit.collider.name);
                            Gizmos.color = Color.red;
                            Gizmos.DrawSphere(hit.point, 1);
                            
                            if (hit.collider.tag == "background")
                            {
                                Vector3 pos = player.transform.position;
                                //ActionList.SetActive(false);
                                player.transform.position = pos;
                                player.GetComponent<HeroStateMachine>().enabled = true;
                                player.GetComponent<HeroStateMachine>().battleStarted = true;
                                player.GetComponent<PlayerController>().enabled = false;
                                GameObject enemy = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)], checkObjects[i].transform.position + new Vector3(0, .7f, 0), Quaternion.identity) as GameObject;
                                loc = true;
                                return;
                            }
                        }
                    }
                    
                    /*while (!loc)
                    {
                        if (Physics.Linecast(cam.transform.position, board.tileGrid[cr.row + 2, cr.columns].transform.position, out hit))
                        {
                            Debug.DrawLine(cam.transform.position, board.tileGrid[cr.row + 2, cr.columns].transform.position, Color.blue, 500);
                            Debug.Log(hit.collider.name);
                            if (hit.collider.tag == "background")
                            {
                                GameObject enemy = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)], hit.collider.transform.position + new Vector3(0, .7f, 0), Quaternion.identity) as GameObject;
                                return;
                            }
                            
                        }
                        if (Physics.Linecast(cam.transform.position, board.tileGrid[cr.row - 2, cr.columns].transform.position, out hit))
                        {
                            Debug.DrawLine(cam.transform.position, board.tileGrid[cr.row + 2, cr.columns].transform.position, Color.blue, 500);
                            Debug.Log(hit.collider.name);
                            if (hit.collider.tag == "background")
                            {
                                GameObject enemy = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)], hit.collider.transform.position + new Vector3(0, .7f, 0), Quaternion.identity) as GameObject;
                                return;
                            }
                            
                        }
                        if (Physics.Linecast(cam.transform.position, board.tileGrid[cr.row, cr.columns + 2].transform.position, out hit))
                        {
                            Debug.DrawLine(cam.transform.position, board.tileGrid[cr.row + 2, cr.columns].transform.position, Color.blue, 500);
                            Debug.Log(hit.collider.name);
                            if (hit.collider.tag == "background")
                            {
                                GameObject enemy = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)], hit.collider.transform.position + new Vector3(0, .7f, 0), Quaternion.identity) as GameObject;
                                return;
                            }
                            
                        }
                        if (Physics.Linecast(cam.transform.position, board.tileGrid[cr.row, cr.columns - 2].transform.position, out hit))
                        {
                            Debug.DrawLine(cam.transform.position, board.tileGrid[cr.row + 2, cr.columns].transform.position, Color.blue, 500);
                            Debug.Log(hit.collider.name);
                            if (hit.collider.tag == "background")
                            {
                                GameObject enemy = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)], hit.collider.transform.position + new Vector3(0, .7f, 0), Quaternion.identity) as GameObject;
                                return;
                            }
   
                        }
                        loc = true;
                    }*/

                    if (!loc)
                    {
                        Destroy(bm);
                        created = false;
                        state = GameState.WORLDSTATE;
                    }
                    
                    //GameObject enemy = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)], , Quaternion.identity) as GameObject;
                }
                break;
            case (GameState.WORLDSTATE):
                player.GetComponent<PlayerController>().enabled = true;
                player.GetComponent<HeroStateMachine>().enabled = false;
                break;
        }
	}
    
}
