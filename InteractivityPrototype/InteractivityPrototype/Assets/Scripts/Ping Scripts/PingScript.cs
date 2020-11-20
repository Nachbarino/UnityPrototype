using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class PingScript : MonoBehaviour
{
    public int scaleFactor;

    private TextMeshPro distanceText;
    private PhotonView pv;
    private Camera myCamera;
    private Vector3 initalScale;

    private void Awake()
    {
        distanceText = transform.GetChild(transform.childCount-1).GetComponent<TextMeshPro>();
        pv = GetComponent<PhotonView>();
        initalScale = transform.localScale;

        Camera[] cameras = FindObjectsOfType<Camera>();
        foreach(Camera cam in cameras)
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
        float size = (myCamera.transform.position - transform.position).magnitude/scaleFactor;
        transform.localScale = new Vector3(size, size, size);

        transform.LookAt(myCamera.transform);


        // updates the distance Text 
        distanceText.text = distance + " M";
        distanceText.transform.rotation = Quaternion.LookRotation(distanceText.transform.position - myCamera.transform.position);
    }

}
