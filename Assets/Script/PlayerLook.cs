using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{   
    [SerializeField] Transform camLook;
    float camRotX = 0f;
    void Start(){
        Cursor.visible = false;
    }
    void Update()
    {   
        Vector2 mouseVector = new Vector2(
            Input.GetAxisRaw("Mouse X") * 100f * Time.deltaTime,
            Input.GetAxisRaw("Mouse Y") * 100f * Time.deltaTime
            );
        
        transform.Rotate(Vector3.up, mouseVector.x);
        camRotX -= mouseVector.y;
        camRotX = Mathf.Clamp(camRotX,-90f,90f);
        camLook.localRotation = Quaternion.Euler(camRotX,0f,0f);
    }
}
