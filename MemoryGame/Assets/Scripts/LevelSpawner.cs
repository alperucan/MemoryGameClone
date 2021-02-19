using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public static LevelSpawner instance;
    public List<Circle> pooledObjects;
    public Circle objectToPool;
    public int amountToPool;
   

    public int LeftEdgeX = -28;
    public int RightEdgeX = 28;
    public int topEdgeY = 8;
    public int botEdgeY = -8;

    private void Awake()
    {
        amountToPool = 10;
        instance = this;
        generateCircle();
    }

    private void Start()
    {
        setTransforms();




    }
    private void Update()
    {
       
      //  ColChange();

    }
    public void generateCircle() 
    {
        pooledObjects = new List<Circle>();
        
        for (int i = 0; i < amountToPool; i++)
        {
            Circle tmp;
            int x = Random.Range(LeftEdgeX, RightEdgeX);
            int y = Random.Range(topEdgeY, botEdgeY);

            Vector3 pos = new Vector3(x, y, 0);

            tmp = Instantiate(objectToPool);
            tmp.transform.position = pos;
            
            

            pooledObjects.Add(tmp) ;
           

        }

    }
  

    public Circle GetPooledObject(int i) 
    {
        return pooledObjects[i];
    }

    public void setTransforms() 
    {
        for (int i = 0; i < amountToPool; i++)
        {
            Vector3 pos1 = pooledObjects[i].transform.position;
            for (int j = i+1; j < amountToPool; j++) {
                Vector3 pos2 = pooledObjects[j].transform.position;
                if (Mathf.Abs(pos1.x - pos2.x) <= 8) 
                {
                    pooledObjects[j].transform.position = new Vector3(pos2.x + 8, pooledObjects[j].transform.position.y, pooledObjects[j].transform.position.z);
                    
                }
                if (Mathf.Abs(pos1.y - pos2.y) <= 8)
                {
                    pooledObjects[j].transform.position = new Vector3(pooledObjects[j].transform.position.x, pos2.y+8, pooledObjects[j].transform.position.z);
                }


            }

        }
    }

 
}
