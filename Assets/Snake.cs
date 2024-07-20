using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour {
    
	// Current Movement Direction (by default it moves to the right)
    Vector2 dir = Vector2.right;
	// Keep Track of Tail
	List<Transform> tail = new List<Transform>();
	// Did the snake eat something?
	bool ate = false;
	// Tail Prefab
	public GameObject tailPrefab;
	// Food Prefab
    public GameObject foodPrefab;
	// Food Value
	public int foodValue = 1;
	// isKeyPressed // bugfix 001 
	private bool isKeyPressed = false;

    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    // Use this for initialization
    void Start () {
        // Move the Snake every 300ms
        InvokeRepeating("Move", 0.25f, 0.25f); 
		Spawn();
    }
   
    // Update is called once per Frame
	void Update() {
        if (!PauseControl.gameIsPaused)
		{			
			// Move in a new Direction?
			if (Input.GetKey(KeyCode.RightArrow) && (dir!=Vector2.left) && !isKeyPressed)
				{ dir = Vector2.right; isKeyPressed = true; }
			else if (Input.GetKey(KeyCode.DownArrow) && (dir!=Vector2.up) && !isKeyPressed ) 
				{ dir = Vector2.down; isKeyPressed = true; }    
			else if (Input.GetKey(KeyCode.LeftArrow) && (dir!=Vector2.right) && !isKeyPressed)
				{ dir = Vector2.left; isKeyPressed = true; }
			else if (Input.GetKey(KeyCode.UpArrow) && (dir!=Vector2.down) && !isKeyPressed)
				{ dir = Vector2.up; isKeyPressed = true; }
		}
	}
   
    void Move() {

		// Save current position (gap will be here)
		Vector2 v = transform.position;

		// Move head into new direction (now there is a gap)
		transform.Translate(dir);

		// Ate something? Then insert new Element into gap
		if (ate) {
			// Load Prefab into the world
			GameObject g =(GameObject)Instantiate(tailPrefab,
												v,
												Quaternion.identity);

			// Keep track of it in our tail list
			tail.Insert(0, g.transform);

			// Update score
			ScoreScript.ScoreValue += foodValue;		    

			// Reset the flag
			ate = false;
		}
		// Do we have a Tail?
		else if (tail.Count > 0) {
			// Move last Tail Element to where the Head was
			tail.Last().position = v;
			// Add to front of list, remove from the back
			tail.Insert(0, tail.Last());
			tail.RemoveAt(tail.Count-1);
		}

		isKeyPressed = false; // bugfix 001
	}

	void OnTriggerEnter2D(Collider2D coll) {
		// Food?
		if (coll.name.StartsWith("FoodPrefab")) {
			// Get longer in next Move call
			ate = true;		
			// Remove the Food
			Destroy(coll.gameObject);
			// Play eat sound
			SoundManagerScript.PlaySound("eat");
			// Spawn the new Food
			Spawn();
		}
		// Collided with Tail or Border
		else {		
			// Play death sound
			SoundManagerScript.PlaySound("death");
			// Reset the game
			Reset();
		}
	}

	void Spawn() {
		bool collision = true; 
		int x = 0; int y = 0;

		while (collision)
		{
			// x position between left & right border
			x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
			// y position between top & bottom border
			y = (int)Random.Range(borderBottom.position.y, borderTop.position.y);
			// check if there is a collision 				
			collision = null != Physics2D.OverlapCircle(new Vector2(x, y), 2.0f);							
		}         

        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation
    }

	void Reset() {
		SceneManager.LoadScene( SceneManager.GetActiveScene().name);
	}
}