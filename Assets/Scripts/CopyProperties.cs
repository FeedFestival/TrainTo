using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CopyProperties : MonoBehaviour
{
    
    public string GetProperties() {

        Transform transform = this.transform;

        return 
      @"x: " + transform.position.x + @"
        y: " + transform.position.y + @"
        z: " + transform.position.z + @"
        
        ";
    }
}
