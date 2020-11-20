using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private LineRenderer lr;
    private PhotonView pv;
    private Transform destination;
    private bool alreadyPicked = false;

    //private Transform pickedObject;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        pv = GetComponent<PhotonView>();
       Debug.Log(transform.parent.parent.childCount);
        destination = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine && Input.GetKeyDown(KeyCode.L))
        {
            pv.RPC("RPC_EnableLaser", RpcTarget.AllBuffered);
        }

        
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {

            if(pv.IsMine&& Input.GetKeyDown(KeyCode.Mouse0) && hit.transform.tag == "WorkTool" && !alreadyPicked)
            {
                Transform pickedObject = hit.transform;
                PhotonView pvOfPickedObject = pickedObject.GetComponent<PhotonView>();
                pv.RPC("RPC_PickUpObject", RpcTarget.All, pvOfPickedObject.ViewID);
                Debug.Log("Already true");
                alreadyPicked = true;
            }

            if (pv.IsMine && Input.GetKeyDown(KeyCode.Mouse0) && hit.transform.tag == "Floor")
            {
               pv.RPC("RPC_CreateArrow", RpcTarget.AllBuffered,hit.point.x, hit.point.y, hit.point.z);
            }

            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, transform.forward * 5000);

        if(pv.IsMine && alreadyPicked && Input.GetKeyDown(KeyCode.Mouse1))
        {
            PhotonView pvOfPickedObject = transform.GetChild(0).GetChild(0).GetComponent<PhotonView>();
            pv.RPC("RPC_LeaveObject", RpcTarget.AllBuffered, pvOfPickedObject.ViewID);
            alreadyPicked = false;
            Debug.Log("Already false");
        }
    }

   
    [PunRPC]
    void RPC_EnableLaser()
    {  
            lr.enabled = !lr.enabled;  
    }

    [PunRPC]
    void RPC_PickUpObject(int viewID)
    {
        Transform pickedObject = PhotonView.Find(viewID).transform;
        pickedObject.GetComponent<Rigidbody>().useGravity = false;
        pickedObject.GetComponent<Rigidbody>().isKinematic = true;
        pickedObject.position = destination.position;
        pickedObject.parent = destination;
    }

    [PunRPC]
    void RPC_LeaveObject(int viewID)
    {
        Transform pickedObject = PhotonView.Find(viewID).transform;
        pickedObject.parent = null;
        pickedObject.GetComponent<Rigidbody>().useGravity = true;
        pickedObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    [PunRPC]
    void RPC_CreateArrow(float posX, float posY, float posZ)
    {
        Vector3 pos = new Vector3(posX,posY,posZ);
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "arrow"),pos , Quaternion.identity, 0);
    }
}