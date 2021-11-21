using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Item Neighbors_left;
    public Item Neighbors_left_up;
    public Item Neighbors_left_down;

    public Item Neighbors_right;
    public Item Neighbors_right_up;
    public Item Neighbors_right_down;
    public Item Neighbors_up;
    public Item Neighbors_down;

    public int x, y;
    public bool isEmpty;
    public int width, heigh;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void verifyNeighbors()
    {
        if (y - 1 >= 0 )
        {
            Neighbors_up = GenerateObjects.matrixItem[x, y - 1];
        } 
        if (x - 1 >= 0 )
        {
            Neighbors_left = GenerateObjects.matrixItem[x - 1, y];
        }
        if (x + 1 < width)
        {
            Neighbors_right = GenerateObjects.matrixItem[x + 1, y];
        }
        if (y + 1 < heigh)
        {
            Neighbors_down = GenerateObjects.matrixItem[x, y +1];
        }
        if (y + 1 < heigh && x - 1 >=0)
        {
            Neighbors_left_down = GenerateObjects.matrixItem[x-1, y + 1];
        }
        if (y - 1 >= 0 && x - 1 >= 0)
        {
            Neighbors_left_up = GenerateObjects.matrixItem[x - 1, y - 1];
        }

        if (x + 1 < width && y - 1 >= 0)
        {
            Neighbors_right_up = GenerateObjects.matrixItem[x + 1, y-1];
        }
        if (x + 1 < width && y + 1 < heigh)
        {
            Neighbors_right_down = GenerateObjects.matrixItem[x + 1, y+1];
        }
    }
    public GameObject CheckNeighbours()
    {

        /* if (Neighbors_left.isEmpty &&
             Neighbors_down.isEmpty &&
             Neighbors_up.isEmpty &&
             Neighbors_right.isEmpty &&
             Neighbors_left_down.isEmpty &&
             Neighbors_left_up.isEmpty &&
             Neighbors_right_up.isEmpty &&
             Neighbors_right_down.isEmpty 
             )
         {*/
        if (Neighbors_left != null)
            if (Neighbors_left.isEmpty)
                Neighbors_left.isEmpty = false;
        if (Neighbors_down != null)
            if (Neighbors_down.isEmpty)
                Neighbors_down.isEmpty = false;
        if (Neighbors_up != null)
            if (Neighbors_up.isEmpty)
                Neighbors_up.isEmpty = false;
        if (Neighbors_right != null)
            if (Neighbors_right.isEmpty)
                Neighbors_right.isEmpty = false;
        if (Neighbors_left_down != null)
            if (Neighbors_left_down.isEmpty)
                Neighbors_left_down.isEmpty = false;
        if (Neighbors_left_up != null)
            if (Neighbors_left_up.isEmpty)
                Neighbors_left_up.isEmpty = false;
        if (Neighbors_right_up != null)
            if (Neighbors_right_up.isEmpty)
                Neighbors_right_up.isEmpty = false;
        if (Neighbors_right_down != null)
            if (Neighbors_right_down.isEmpty)
                Neighbors_right_down.isEmpty = false;

        isEmpty = false;
        return gameObject;
        /*    }
            return null;*/
    }
}
