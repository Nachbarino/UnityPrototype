using UnityEngine;
using System.Collections;
using Photon.Pun;
public class ActivateChest : MonoBehaviour {

	public Transform lid, lidOpen, lidClose;	// Lid, Lid open rotation, Lid close rotation
	public float openSpeed = 5F;				// Opening speed
	public bool canClose;						// Can the chest be closed
	
	[HideInInspector]
	public bool _open;							// Is the chest opened
	public PhotonView photonView;

	void Start()
	{
		photonView = GetComponent<PhotonView>();
	}
	void Update () {
		if(_open){
			ChestClicked(lidOpen.rotation);
		}
		else{
			ChestClicked(lidClose.rotation);
		}
	}
	
	// Rotate the lid to the requested rotation
	void ChestClicked(Quaternion toRot){
		if(lid.rotation != toRot){
			lid.rotation = Quaternion.Lerp(lid.rotation, toRot, Time.deltaTime * openSpeed);
		}
	}
	
	void OnMouseDown(){
		if(canClose)
		{ 
			//_open = !_open; 
			photonView.RPC("toggleState", RpcTarget.AllBuffered);
		}
		else _open = true;
	}

	[PunRPC]
	public void toggleState()
	{
		_open = !_open;
	}
}
