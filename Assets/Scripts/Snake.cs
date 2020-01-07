using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour
{

    // Current movement direction
    // (by default it moves to the right)
    Vector2 dir = Vector2.right;

    // Keep track of tail
    List<Transform> tail = new List<Transform>();
    
    // Did snake eat somethimg?
    bool ate = false;

    //Did user die?
    bool isDied = false;

    // Tail Prefab
    public GameObject tailPrefab;

    // Game Over menu
    public GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Move the snake every 300ms
        InvokeRepeating("Move", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDied)
        {
            // Move in a new direction with user input

            if(Input.GetKey(KeyCode.RightArrow))
            {
                if(dir.x != -1)
                {
                dir = Vector2.right;
                }
            }
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                if(dir.y != +1)
                {
                dir = -Vector2.up;
                }      // '-up' means 'down'
            }
            else if(Input.GetKey(KeyCode.LeftArrow))
            {
                if(dir.x != +1)
                {
                dir = -Vector2.right;
                }   // '-right' means 'left'
            }
            else if(Input.GetKey(KeyCode.UpArrow))
            {
                if(dir.y != -1)
                {
                dir = Vector2.up;
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.R)){
				//clear the tail
				tail.Clear();

				//reset to origin
				transform.position = new Vector3(0, 0, 0);

				//make snake alive
				isDied = false;
			}
        }
    }

    // Do Movement Stuff
    void Move()
    {
        if(!isDied)
        {
            // Save current position (gap will be here)
            Vector2 v = transform.position;

            // Move head into new direction
            transform.Translate(dir);
            
            // Ate something? Then insert new element into gap
         if(ate)
            {
                //Load Prefab onto the world
                GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

                // Keep track of it in out tail list
                tail.Insert(0, g.transform);
                // Play eat Apple sound
                SoundManagerScript.PlaySound ("apple");

                //Reset the flag
                ate = false;

               
            }
            else if(tail.Count > 0)
            {
                    // Do we have a Tail?
					// Move last Tail Element to where the Head was
					tail.Last ().position = v;

					// Add to front of list, remove from the back
					tail.Insert (0, tail.Last ());
					tail.RemoveAt (tail.Count - 1);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if(coll.name.StartsWith("FoodPrefab"))
        {
            ate = true;

            // Remove food
            Destroy(coll.gameObject);
            ScoreScript.scoreValue += 50;
        }
        
        // Collided with Tail or Border
        else
        {
            isDied = true;
            gameOverMenu.SetActive(true);  
            // Play Game Over sound
            SoundManagerScript.PlaySound ("gameOver");
        }
    }
}
