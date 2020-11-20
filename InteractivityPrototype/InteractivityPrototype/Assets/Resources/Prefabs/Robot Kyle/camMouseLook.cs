using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;

public class camMouseLook : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0F;
    public float smoothing = 2.0F;

    GameObject character;
    GameObject avatar;


    // Start is called before the first frame update
    void Start()
    {
        character = this.transform.parent.gameObject; 
        avatar = character.transform.GetChild(0).gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetKeyDown("escape") && Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if(Input.GetKeyDown("escape") && Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        if(character.GetComponent<PhotonView>().IsMine && Cursor.lockState == CursorLockMode.Locked)
        {
            MouseMovement();
        }
    }

    void MouseMovement()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f/smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f/smoothing);
        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp (mouseLook.y, -90F, 90F); 

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);   
    }

    void OnPreRender()
    {
        avatar.GetComponent<SkinnedMeshRenderer>().enabled = false;
    }
}
