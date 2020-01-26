using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CopyProperties : MonoBehaviour
{

    public string GetProperties()
    {
        RectTransform rT = GetComponent<RectTransform>();
        RectTransform crT = rT.GetChild(0).GetComponent<RectTransform>();

        TextEditor te = new TextEditor();
        te.text = @"DialogBoxType = DialogBoxType." + gameObject.name + @",
posX = " + rT.anchoredPosition3D.x + @"f, posY = " + rT.anchoredPosition3D.y + @"f, posZ = " + rT.anchoredPosition3D.z + @"f, width = " + rT.sizeDelta.x + @"f, height = " + rT.sizeDelta.y + @"f, sizeX = " + rT.localScale.x + @"f, sizeY = " + rT.localScale.y + @"f, sizeZ = " + rT.localScale.z + @"f,
anchorsMinX = " + rT.anchorMin.x + @"f, anchorsMinY = " + rT.anchorMin.y + @"f, anchorsMaxX = " + rT.anchorMax.x + @"f, anchorsMaxY = " + rT.anchorMax.y + @"f, pivotX = " + rT.pivot.x + @"f, pivotY = " + rT.pivot.y + @"f, 
rotationX = " + rT.eulerAngles.x + @"f, rotationY = " + rT.eulerAngles.y + @"f, rotationZ = " + rT.eulerAngles.z + @"f, childWidth = " + crT.sizeDelta.x + @"f, childHeight = " + crT.sizeDelta.y + @"f";
        te.SelectAll();
        te.Copy();
        return te.text;
    }
}
