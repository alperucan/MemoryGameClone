using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public static LevelSpawner instance;
    public List<Circle> pooledObjects;
    public Circle objectToPool;
    public int amountToPool;


    private int LeftEdgeX = -42;
    private int RightEdgeX = 35;
    private int topEdgeY = 22;
    private int botEdgeY = -21;
    private int zPos = 26;
    private void Awake()
    {
        amountToPool = 10;
        instance = this;
        generateCircle();
    }

    private void Start()
    {
      

    }
   
    /// <summary>
    /// Generate Circles
    /// </summary>
    private void generateCircle()
    {
        pooledObjects = new List<Circle>();

        bool overlap = false;
        int projection = 0;
        while (pooledObjects.Count < amountToPool)
        {
            int x = Random.Range(LeftEdgeX, RightEdgeX);
            int y = Random.Range(botEdgeY, topEdgeY);
            Vector3 pos = new Vector3(x, y, zPos);
            Circle circle;
          
           

            overlap = false;
            for (int j = 0; j < pooledObjects.Count; j++) {
                Circle tmp = pooledObjects[j];
                int d = (int)Vector3.Distance(pos,tmp.transform.position);
                if (d < 14f    ) //14f= 2R
                {
                    overlap = true;
                }
                if (pos.x > -15 && pos.x < 15) { //TurnTextin ustune gelmesin
                   // Debug.Log("x error " + pos.x);
                    overlap = true;
                }
                if (pos.y > -5 && pos.y < 5) //TurnTextin ustune gelmesin
                {
                    //Debug.Log("Y error " + pos.y);
                    overlap = true;
                }
            }

            if (!overlap) {
                circle = Instantiate(objectToPool);
                circle.transform.position = pos;
                pooledObjects.Add(circle);
              }
            projection++;
            if(projection >5000) break;
        }

    }


    public Circle GetPooledObject(int i)
    {
        return pooledObjects[i];
    }

   


}
