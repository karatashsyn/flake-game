using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class flakeScript : MonoBehaviour
{
    public Rigidbody2D headRigidBody;
    public float HORIZONTAL_ACC = 0.24f;
    public float VERCTICAL_ACC = 0.24f;
    public float STOPPING_ACC = 0.16f;
    public float SPEED_X = 2f;
    public float MAX_X = 4f;
    public float MAX_Y = 14f;
    public float MIN_Y = -14f;
    public List<GameObject> bodySegments;
    public float segmentDelay = 0.002f;
    public Coroutine followCoroutine;
    private int numOfBodySegments = 100;
    public GameLogicScript gameLogic;
    public ParticleSystem particleSystem;
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Block")
        {
            particleSystem.transform.position=headRigidBody.transform.position;
            particleSystem.Play();
            gameLogic.fail();
            Debug.Log("Snake collided with a block. Game over!");
            // Add your game over logic here, such as restarting the level or showing a game over screen.
        }
    }

    public void stopTheSnake() {
        headRigidBody.Sleep();
        StopCoroutine(followCoroutine);
    }

    public void startTheSnake()
    {
        headRigidBody.WakeUp();
        StartCoroutine(FollowSegments());
    }

    void accelerate()
    {
        if (headRigidBody.velocity.y < MAX_Y)
        {
            headRigidBody.velocity = new Vector2(headRigidBody.velocity.x , headRigidBody.velocity.y + VERCTICAL_ACC);
        }
        if (headRigidBody.velocity.x > MAX_X) { headRigidBody.velocity = new Vector2(MAX_X, headRigidBody.velocity.y); }
    }

    void slowDown()
    {
        if (headRigidBody.velocity.x - SPEED_X > HORIZONTAL_ACC)
        {
            headRigidBody.velocity = new Vector2(headRigidBody.velocity.x - STOPPING_ACC, headRigidBody.velocity.y);
        }
        else
        {
            headRigidBody.velocity = new Vector2(SPEED_X, headRigidBody.velocity.y);
        }
    }

    IEnumerator FollowSegments()
    {
        yield return new  WaitForSeconds(segmentDelay);
        Vector3 prev =  bodySegments[0].transform.position;
        for (int i = 1; i < numOfBodySegments; i++)
        {
            Vector3 temp = bodySegments[i].transform.position;
            bodySegments[i].transform.position = prev;
            prev = temp;
        }
        followCoroutine = StartCoroutine(FollowSegments());
    }

    // Start is called before the first frame update
    void Start()

    {
        gameLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogicScript>();
        bodySegments.Capacity = numOfBodySegments;
        bodySegments.Add(GameObject.Find("Head"));
        for (int i = 1; i <= numOfBodySegments - 2; i++)
        {
            bodySegments.Add(GameObject.Find("seg_" + (i)));
        }
         bodySegments.Add(GameObject.Find("Tail"));
        
        headRigidBody.velocity = new Vector2(SPEED_X, 0);   
        followCoroutine = StartCoroutine(FollowSegments());
    }


    void Update()
    {
        if (gameLogic.GAME_STATE == GameLogicScript.State.PLAYING)
        {
            if (Input.GetKey(KeyCode.Space) == true)
            {
                accelerate();
            }
            else
            {
                slowDown();
            }
            if (headRigidBody.velocity.y < MIN_Y) { headRigidBody.velocity = new Vector2(headRigidBody.velocity.x, MIN_Y); }
        }
         
    }

}

