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
        //Dogru tiklanan tekrar "Circle" olur
        if (this.gameObject.CompareTag("FlashedCircles"))
        {
            isPressed = true;
           
            this.gameObject.GetComponent<Circle>().ChangeColor();
            StartCoroutine(waitOneSec());
            this.gameObject.tag = "Circle";
            int count = gameController.flashedCircles.IndexOf( gameObject.GetComponent<Circle>() );
            
           
            
            if (count < GameController.level-1) 
            {
                gameController.flashedCircles[count + 1].gameObject.tag = "FlashedCircles";  
            }
            GameController.point += (100 / GameController.level );
            gameController.levelSpawner.pooledObjects.Add(gameObject.GetComponent<Circle>());
            
        }
        else
        {
            isPressed = true;
           
            this.gameObject.GetComponent<Circle>().Change2Red();
            StartCoroutine(waitOneSec());
            gameController.gameOver = false;
            GameController.point -= (100 / GameController.level );
            //Restartla!
            gameController.FalseClick();
        }

    }

    IEnumerator waitOneSec()
    {
       
        yield return new WaitForSeconds(1.5f);
        this.gameObject.GetComponent<Circle>().ResetColor();

    }
   
}
