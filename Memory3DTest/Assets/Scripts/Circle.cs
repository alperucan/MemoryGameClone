using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
    public SpriteRenderer circleImage;
   
    public GameController gameController;
   


    private void Start()
    {
        
        circleImage = GetComponent<SpriteRenderer>();
        
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    public void ChangeColor()
    {
        circleImage.color = Color.blue;

    }
    public void ResetColor()
    {
        circleImage.color = Color.white;
    }
    public void Change2Red()
    {
        circleImage.color = Color.red;
    }
    void OnMouseDown()
    {
        //if clicked circle member of flashedCircles then okey else error!
        //Dogru tiklanan tekrar "Circle" olur
        if (this.gameObject.CompareTag("FlashedCircles"))
        {

           
            this.gameObject.GetComponent<Circle>().ChangeColor();
            StartCoroutine(waitOneSec());
            this.gameObject.tag = "Circle";
            int count = gameController.flashedCircles.IndexOf(gameObject.GetComponent<Circle>());
            



            if (count < GameController.level - 1)
            {
                gameController.flashedCircles[count + 1].gameObject.tag = "FlashedCircles";
            }
            // GameController.point += (100 / GameController.level);
           
            GameController.userTrue++;
          
            gameController.levelSpawner.pooledObjects.Add(gameObject.GetComponent<Circle>());
            
        }
        else
        {
           

            this.gameObject.GetComponent<Circle>().Change2Red();
            StartCoroutine(waitOneSec());
            gameController.gameOver = false;
            GameController.userFalse++;
           // GameController.point -= (100 / GameController.level);
            //Restartla!
            StartCoroutine(gameController.FalseClick());
        }

    }

    IEnumerator waitOneSec()
    {

        yield return new WaitForSeconds(1.5f);
        this.gameObject.GetComponent<Circle>().ResetColor();

    }

}
