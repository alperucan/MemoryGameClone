using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
     public LevelSpawner levelSpawner;
     public int level;
     public bool pcTurn;
     public List<int> circleOrderNumbers;
     private int yananCircleNum;
     public List<Circle> flashedCircles;
     public bool userTurn;
     public Text TurnText;
     public bool gameOver;
     public int point;

     public Image PointBar;

     void Start()
     {
        point = 0;
        gameOver = false;
        pcTurn = true;
        userTurn = false;
        circleOrderNumbers = new List<int>();
        
        if (pcTurn)
        {
            TurnText.text = "BILGISAYARIN SIRASI";
            StartCoroutine(flashCircles());
            
        }
      


    }
    private void Update()
    {
        if (!gameOver && !pcTurn) { 
            changeTurn();
            fillPointBar();
        }
        
    }


    //
    private IEnumerator flashCircles() 
    {
        if (yananCircleNum < level) 
        {
             int a = Random.Range(0, levelSpawner.pooledObjects.Count);
             circleOrderNumbers.Add(a);
            //Debug.Log(a);
            //Debug.Log(levelSpawner.pooledObjects.Count);
            //Debug.Log(levelSpawner.pooledObjects[a]);
             levelSpawner.pooledObjects[a].ChangeColor();
           
             yield return new WaitForSeconds(1.5f);
             levelSpawner.pooledObjects[a].ResetColor();
            
             flashedCircles.Add(levelSpawner.pooledObjects[a]);
            flashedCircles[0].tag = "FlashedCircles";
            levelSpawner.pooledObjects.RemoveAt(a);

             pcTurn = false;
             yananCircleNum++;
             //Debug.Log("yanancircle" + yananCircleNum);
             StartCoroutine(flashCircles());
        }
       
    }

    private void changeTurn() {
        TurnText.text = "SENIN SIRAN";

    }

    public void fillPointBar() {

        PointBar.fillAmount = (float)point / 100;
    }
   


}
