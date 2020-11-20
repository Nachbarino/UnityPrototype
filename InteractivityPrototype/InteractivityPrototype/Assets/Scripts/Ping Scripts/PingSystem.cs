using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PingSystem : MonoBehaviour
{
    private PhotonView PV;
    public Transform rayOrigin;
    // Start is called before the first frame update

    public Material visibleThroughWallsMaterial;
    private Material originalChestMaterial;


    void Start()
    {
        PV = GetComponent<PhotonView>();
        GameObject tempChest = GameObject.FindGameObjectWithTag("Chest");
        originalChestMaterial = tempChest.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        // Mittlere Maustaste
        if (Input.GetMouseButtonDown(2))
        {
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {

                if (hit.transform.tag == "Chest")
                {
                    GameObject chest = hit.transform.gameObject;
                    PhotonView pvOfChest = chest.GetComponent<PhotonView>();
                    PV.RPC("RPC_PingChest", RpcTarget.All,pvOfChest.ViewID);
                }

                if(hit.transform.tag == "WorkTool")
                {
                    GameObject tool = hit.transform.gameObject;
                    PhotonView pvOfTool = tool.GetComponent<PhotonView>();
                    PV.RPC("RPC_PingTool", RpcTarget.All, pvOfTool.ViewID);
                }
            }
        }



        
    }

    [PunRPC]
    void RPC_PingTool(int viewID)
    {
        GameObject tool = PhotonView.Find(viewID).transform.gameObject;
        GameObject toolPing = tool.transform.Find("ToolPing").gameObject;

        toolPing.SetActive(!toolPing.activeSelf);

    }

    [PunRPC]
    void RPC_PingChest(int viewID)
    {
        GameObject chest = PhotonView.Find(viewID).transform.gameObject;
        GameObject chestPing = chest.transform.Find("ChestPing").gameObject;

        if (!chestPing.activeSelf) // Ping is visible
        {
            chest.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material=visibleThroughWallsMaterial;
            chest.transform.GetChild(3).GetChild(0).GetComponent<MeshRenderer>().material = visibleThroughWallsMaterial;
        }
        else
        {
            chest.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = originalChestMaterial;
            chest.transform.GetChild(3).GetChild(0).GetComponent<MeshRenderer>().material = originalChestMaterial;
        }

        chestPing.SetActive(!chestPing.activeSelf);
        Debug.Log("State of Ping "+!chestPing.activeSelf);
       // pingCoroutine(chestPing);
    }
    //TODO Zeitproblem lösen
    IEnumerator pingCoroutine(GameObject ping)
    {
        yield return new WaitForSeconds(5);
        ping.SetActive(false);
    }
}
