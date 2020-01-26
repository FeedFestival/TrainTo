using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public DialogBox[] DialogBoxes;
    public string[] Sentences;
}

public class DialogBox
{
    public DialogBoxType DialogBoxType;
    public float posX;
    public float posY;
    public float posZ;
    //-------------------------------------
    public float width;
    public float height;
    //-------------------------------------
    public float sizeX;
    public float sizeY;
    public float sizeZ;
    //-------------------------------------
    public float anchorsMinX;
    public float anchorsMinY;
    //-------------------------------------
    public float anchorsMaxX;
    public float anchorsMaxY;
    //-------------------------------------
    public float pivotX;
    public float pivotY;
    //-------------------------------------
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    //-------------------------------------
    public float childWidth;
    public float childHeight;

}

public enum DialogBoxType
{
    dbs
}
