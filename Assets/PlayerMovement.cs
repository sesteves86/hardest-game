using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed; 
    public Rigidbody2D rb;
    //public GameObject[] respawnObj;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Coin":
                col.gameObject.GetComponent<Renderer>().enabled = false;
                //Destroy(col.gameObject);
                break;

            case "Enemy":
                foreach(var coin in GameObject.FindGameObjectsWithTag("Coin"))
                {
                    coin.GetComponent<Renderer>().enabled = true;
                }

                //go back to the start
                var respawns = GameObject.FindGameObjectsWithTag("Respawn");
                Debug.Log("Collision detected. Number of respawn areas:" + respawns.Length);
                if (respawns.Length > 0)
                {
                    rb.transform.position = respawns[0].transform.position;
                }
                //var respawnArea = GameObj.FindGameObjectsWithTag("Respawn");
                //rb.transform.position = respawnObj.position;
                break;

            case "Finish":
                var allCoins = GameObject.FindGameObjectsWithTag("Coin");
                var remainingCoins = Array.FindAll(allCoins, c => c.GetComponent<Renderer>().enabled);

                if (remainingCoins.Length > 0)
                {
                    break;
                }

                int currentLevel = SceneManager.GetActiveScene().buildIndex;
                Debug.Log("Finished level " + currentLevel);
                SceneManager.LoadScene(currentLevel + 1);

                break;
            default:
                break;
        }

    }
}
