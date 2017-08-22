using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    float speed = 1f;
    Rigidbody rb;
    GameObject player;
    Vector3 forward, right;
    Collider collider;
    Animator anim;

    GameObject gameManager;
    GameObject checker;
    GameObject menu;

    void Start() {
        gameManager = GameObject.Find("GameManager");
        Debug.Log(menu);
        checker = GameObject.Find("Checker");
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        anim = player.GetComponent<Animator>();
        player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            Move();
        } else
        {
            anim.SetBool("WalkRight", false);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("idle", true);
        }
        

    }
    void Move()
    {
        anim.SetBool("idle", false);
        Vector3 rightMovement = right * speed * 2 *  Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * speed * 2 * Time.deltaTime * Input.GetAxis("VerticalKey");
        if (Input.GetAxis("HorizontalKey") > 0)
        {
            anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkRight", true);
        }
        else if (Input.GetAxis("HorizontalKey") < 0)
        {
            anim.SetBool("WalkRight", false);
            anim.SetBool("WalkLeft", true);
        }
        else if (Input.GetAxis("VerticalKey") > 0)
        {
            
        }
        else if (Input.GetAxis("VerticalKey") < 0)
        {

        }
        Debug.Log("Horizontal Key: " + Input.GetAxis("HorizontalKey"));
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward = heading;
        checker.transform.position = player.transform.position;
        
        if (collider)
        {
            rb.AddForce(Vector3.zero);
        }
        else
        {
            rb.AddForce((upMovement + rightMovement) * speed, ForceMode.VelocityChange);
        }
        if (rb.velocity.magnitude > 2)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
}
