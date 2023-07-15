using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameLogicScript : MonoBehaviour
{
    public movementScript flake;

    private void Start()
    {
        flake = GameObject.FindGameObjectWithTag("Head").GetComponent<movementScript>();
        flake.headRigidBody.Sleep();
        GAME_STATE = State.INITIAL;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) == true && GAME_STATE == State.INITIAL) {
            flake.initializeSnake();
            GAME_STATE = State.PLAYING;
        }
        
    }




    public enum State {
        INITIAL=0, PLAYING=1, PAUSED=2, FAILED=3
    }

    public State GAME_STATE;

    public void play()
    {
        GAME_STATE = State.PLAYING;
    }


    public void showFailedScreen() { 
    
    }

    public void fail() { 
        GAME_STATE = State.FAILED;
        flake.stopTheSnake();
    }


    public void pause() { }

    public void restart() {  }

}
