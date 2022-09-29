using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseDrag(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.Rotate(Vector3.up * (Input.GetAxisRaw("Mouse X") * -5f));
    }

    private void OnMouseUp(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
