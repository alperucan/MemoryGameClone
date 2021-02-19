using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool isPressed;
    public GameController gameController;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    public void ChangeColor() 
    {
        spriteRenderer.color = Color.blue;
    
    }
    public void ResetColor()
    {
        spriteRenderer.color = Color.white;
    }
    public void Change2Red()
    {
        spriteRenderer.color = Color.red;
    }
    void OnMouseDown()
    {
        //if clicked circle member of flashedCircles then okey else error!
        if (this.gameObject.CompareTag("FlashedCircles"))
        {
            isPressed = true;
            //Debug.Log("a");
            this.gameObject.GetComponent<Circle>().ChangeColor();
            StartCoroutine(waitOneSec());
            this.gameObject.tag = "Circle";
            int count = gameController.flashedCircles.IndexOf(gameObject.GetComponent<Circle>() );
            //Debug.Log(count);
            //gameController.circleOrderNumbers.
            if (count < gameController.level-1) 
            {
                gameController.flashedCircles[count + 1].gameObject.tag = "FlashedCircles";  
            }
            gameController.point += (100 / gameController.level);

        }
        else
        {
            isPressed = true;
            // point --
            this.gameObject.GetComponent<Circle>().Change2Red();
            StartCoroutine(waitOneSec());
            gameController.gameOver = false;
            gameController.point -= (100 / gameController.level);
        }

    }

    IEnumerator waitOneSec()
    {
       
        yield return new WaitForSeconds(1.5f);
        this.gameObject.GetComponent<Circle>().ResetColor();

    }
   
}
