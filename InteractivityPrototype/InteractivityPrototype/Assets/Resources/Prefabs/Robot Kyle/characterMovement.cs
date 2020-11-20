using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Networking;

public class characterMovement : MonoBehaviour
{
    public float movementSpeed;
    //public float rotationSpeed;
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start of characterController");
        photonView = GetComponent<PhotonView>();
 
        if(!photonView.IsMine)
        {
            Camera camera = GetComponentInChildren<Camera>();
            camera.enabled = false;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            BasicMovement();
        }    
    }

    void BasicMovement()
    {
            float translation = Input.GetAxis("Vertical") * movementSpeed;
            float straffe = Input.GetAxis("Horizontal") * movementSpeed;
            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(straffe, 0, translation);
    }
}
