using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class ArrowScript : MonoBehaviour
{
    private TextMeshPro distanceText;
    private PhotonView pv;
    private Camera myCamera;
    private Vector3 initalScale;

    private void Awake()
    {
        distanceText = transform.GetChild(transform.childCount - 1).GetComponent<TextMeshPro>();
        pv = GetComponent<PhotonView>();
        initalScale = transform.localScale;
        pv = GetComponent<PhotonView>();

        Camera[] cameras = FindObjectsOfType<Camera>();
        foreach (Camera cam in cameras)
        {
            PhotonView temp = cam.GetComponent<PhotonView>();
            if (temp.IsMine)
            {
                myCamera = cam;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pingPosition = transform.position;
        Vector3 playerPosition = myCamera.transform.position;
        int distance = (int)Vector3.Distance(pingPosition, playerPosition);
        Debug.Log("Distance =  " + distance);
        if (distance <= 1)
        {
            pv.RPC("RPC_DestroyArrow", RpcTarget.All,pv.ViewID);
        }

        transform.LookAt(myCamera.transform);


        // updates the distance Text 
        distanceText.text = distance + " M";
        distanceText.transform.rotation = Quaternion.LookRotation(distanceText.transform.position - myCamera.transform.position);
    }

    [PunRPC]
    void RPC_DestroyArrow(int viewID)
    {
        Debug.Log("Im in destroy arrow");
        Transform arrow = PhotonView.Find(viewID).transform;
        Destroy(arrow.gameObject);
    }
}
