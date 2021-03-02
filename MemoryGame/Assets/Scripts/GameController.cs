using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
     public LevelSpawner levelSpawner;
     [SerializeField]
     public static int level=2;
     public bool pcTurn;
    public bool userTurn;
    public List<int> circleOrderNumbers;
     private int yananCircleNum;
     public List<Circle> flashedCircles;
     public List<int> playerOrder;

    
     public Text TurnText;
     public bool gameOver;
     public static int point;
     public Text PointArea;
    
     public Image PointBar;

    public void Awake()
    {
        fillPointBar();
        gameOver = false;
        pcTurn = true;
        userTurn = false;
    }
    void Start()
     {
       
        circleOrderNumbers = new List<int>();
        
        if (pcTurn)
        {
            TurnText.text = "BILGISAYARIN SIRASI";
            StartCoroutine(flashCircles());
           
        }
      


    }
    private void Update()
    {
        if (!gameOver && !pcTurn && userTurn) { 
            changeTurn();
            fillPointBar();
            checkAndSetPoint();
        }
        
        
        CheckGameIsOver();


    }
  

   /// <summary>
   /// Change Color circles randomly
   /// </summary>
   /// <returns></returns>
    private IEnumerator flashCircles() 
    {
        if (yananCircleNum < level) 
        {
             int a = Random.Range(0, levelSpawner.pooledObjects.Count);
             circleOrderNumbers.Add(a); //kacinci eleman oldugunu ekle
            //Debug.Log(a);
            //Debug.Log(levelSpawner.pooledObjects.Count);
            //Debug.Log(levelSpawner.pooledObjects[a]);
             levelSpawner.pooledObjects[a].ChangeColor();
           
             yield return new WaitForSeconds(1.5f);
             levelSpawner.pooledObjects[a].ResetColor();
            
             flashedCircles.Add(levelSpawner.pooledObjects[a]); // yanan circleları ayri listeye ekle
             flashedCircles[0].tag = "FlashedCircles";
             levelSpawner.pooledObjects.RemoveAt(a); //yanan circleı listeden cikar

            
             yananCircleNum++;
             //Debug.Log("yanancircle" + yananCircleNum);
             StartCoroutine(flashCircles());
        }
        pcTurn = false;
        userTurn = true;
    }

    private void changeTurn() {
        if ( flashedCircles.Count == level ) 
        {
            
            
            TurnText.text = "SENIN SIRAN";
        }
      

    }

    public void fillPointBar() {

        PointBar.fillAmount = (float)point / 100;
    }
   
    public void CheckGameIsOver()
    {
        //tıklanan objeler levelspawnerdaki pooledobjectlere geri donunce oyun biter. level variableı artar.Scene restartlanir.
        if (levelSpawner.pooledObjects.Count == 10 && !pcTurn) {
            //Debug.Log("Bitti");
            level++;
            pcTurn = true;
            SceneManager.LoadScene("SampleScene");
        }


    }
    public void FalseClick() {
        SceneManager.LoadScene("SampleScene");
    }
    public void checkAndSetPoint() {
        if (point >= 0 && point <= 100) {
            PointArea.text = "% " + point;
        }
        if (point > 100)
        {
            point = 100;
            PointArea.text = "% " + point;
        }
        if (point < 0) {
            point = 0;
            PointArea.text = "% " + point;
        }
    }

}
