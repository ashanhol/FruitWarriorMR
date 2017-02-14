using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitFall : MonoBehaviour, IInputClickHandler
{
    //Variables
    private AudioSource objectSound;
    private GameController gameController;
    
    // Use this for initialization
    void Start()
    {
        //Get GameController
        GameObject gamecontroller = GameObject.FindGameObjectWithTag("GameController");
        gameController = gamecontroller.GetComponent<GameController>();

        //Play falling sound
        objectSound = gameObject.GetComponent<AudioSource>();
        objectSound.Play();

    }

    /*Implements IInputClickHandler, which handles click events. Will add to score and set off bomb.*/
    public void OnInputClicked(InputEventData eventData)
    {
        //Check to see if bomb- deletes all fruits
        if (gameObject.tag.Equals("Bomb"))
        {
            //Play bomb sound
            gameController.BombClear();
        }
        else
        {
            //Add to Score
            gameController.AddScore();

            Destroy(gameObject);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        //Hit the ground
        objectSound.Pause();

        //Destroy before they fall forever
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }

    }
}
