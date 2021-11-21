using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    [SerializeField] private Transform grid_pos;
    [SerializeField] private Item prefabItem;
    public static Item[,] matrixItem;
    public int widthMatrix;
    public int heithhMatrix;
    [SerializeField] int sizeItem;
    [SerializeField] private CreateObjects _createObj;
    


    void Start()
    {
     //   SizeScale();
      
    }

    void Update()
    {
        
    }
    private void SizeScale()
    {

        widthMatrix = (int)Screen.width / 154;
        int tmp = ((int)Screen.height / 100) * 85;
        heithhMatrix = tmp / 154;
    }
    public void GenerateMatrix()
    {
       // SizeScale();
        matrixItem = new Item[widthMatrix, heithhMatrix];
        GenerateGrid();
        SearchNeighbors();
       
    }
    private void GenerateGrid()
    {
        
        for (int y = 0; y < heithhMatrix; y++)
        {
            for (int x = 0; x < widthMatrix; x++)
            {
                Item prefab = Instantiate(prefabItem, grid_pos);
                matrixItem[x, y] = prefab;
                matrixItem[x, y].name = "item  x:" + x + "   y:  " + y;
                prefab.width = widthMatrix;
                prefab.heigh = heithhMatrix;
                prefab.x = x;
                prefab.y = y;
                prefab.isEmpty = true;
            }
        }
    }
    public void SearchNeighbors()
    {
        for (int x = 0; x < widthMatrix; x++)
        {
            for (int y = 0; y < heithhMatrix; y++)
            {
                matrixItem[x, y].verifyNeighbors();
            }
        }
    }
    public GameObject SearchGo(int xx, int yy)
    {

        foreach (var item in matrixItem)
        {
            if (item.x == xx & item.y == yy)
            {
                if (item.isEmpty)
                {
                   
                    return item.CheckNeighbours();

                }
            }
        }
        return null;
    }
    
}
