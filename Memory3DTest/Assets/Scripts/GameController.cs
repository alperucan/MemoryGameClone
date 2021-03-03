using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Timer timer;
    public LevelSpawner levelSpawner;
    [SerializeField]
    public static int level = 2;
    public static int SceneQuestCount=0;
    public static float time;
    public bool pcTurn;
    public bool userTurn;
    public List<int> circleOrderNumbers;
    private int yananCircleNum;
    public List<Circle> flashedCircles;
    public List<int> playerOrder;
    
    public static int userTrue;
    public static int userFalse;

    public static float userTime;
    public float startTime;
    public float endTime;
    


    public Text TurnText;
    public bool gameOver;
    public static float point;
    public Text PointArea;

    public Image PointBar;

    public void Awake()
    {
        
        PointArea.text = "% " + point.ToString("0");
        fillPointBar();
        gameOver = false;
        pcTurn = true;
        userTurn = false;
       
    }
    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        fillPointBar();
       
        circleOrderNumbers = new List<int>();

        if (pcTurn)
        {
            TurnText.text = "BILGISAYARIN SIRASI";
            StartCoroutine(flashCircles());

        }
        //Kullanici sirasinin baslamasi
        startTime = Time.realtimeSinceStartup;
         // Debug.Log("Start Time: " + startTime);

    }
    private void Update()
    {
        setTime();


        if (!gameOver && !pcTurn && userTurn)
        {
           
            changeTurn();
           

        }


        CheckGameIsOver();


    }
    private void setTime()
    {
        time = Time.time;

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

    private void changeTurn()
    {
        if (flashedCircles.Count == level)
        {


            TurnText.text = "SENIN SIRAN";
        }


    }
    public void setPoint()
    {
       // Debug.Log("start: " + startTime);
       // Debug.Log("end: " + endTime);

        userTime = endTime - startTime;
        //Debug.Log("UserTime: " + userTime);
        point = Mathf.Abs( ((userTrue - userFalse)/userTime) * 100);

    }
    public void fillPointBar()
    {

        PointBar.fillAmount = (float)point /100;
    }
    public void setSceneQuestNum() {
        SceneQuestCount++;
    }
    public void CheckGameIsOver()
    {
        //tıklanan objeler levelspawnerdaki pooledobjectlere geri donunce oyun biter. level variableı artar.Scene restartlanir.
      
        
        if (levelSpawner.pooledObjects.Count == 10 && !pcTurn )
        {
            endTime =Time.realtimeSinceStartup;
            // Debug.Log("Bitti"); 
            setSceneQuestNum();
            level++;
            checkAndSetPoint();
            StartCoroutine(FalseClick());
            pcTurn = true;
            

        }


    }
    public IEnumerator FalseClick()
    {
        //Kullanici sirasinin bitmesi
        
       // Debug.Log("end Time: " + endTime);
       
        yield return new  WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }
    public void checkAndSetPoint()
    {
        setPoint();
       // Debug.Log("Point " + point);
        if (point >= 0 && point <= 100)
        {
           
            PointArea.text = "% " + point.ToString("0");
        }
        if (point > 100)
        {
            point = 100;
            PointArea.text = "% " + point.ToString("0");
        }
        if (point < 0)
        {
            point = 0;
            PointArea.text = "% " + point.ToString("0");
        }
        
    }

}
