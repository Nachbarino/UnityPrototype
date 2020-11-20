using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHighlight : MonoBehaviour
{
    private GameObject chest;
    private GameObject Top;
    private GameObject Bottom;

     private Color startcolorTop;
     private Color startcolorBottom;

     void OnMouseEnter()
     {
        chest = transform.gameObject;
        Top = chest.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject;
        Bottom = chest.transform.GetChild(0).gameObject;

        startcolorTop = Top.GetComponent<Renderer>().material.color;
        startcolorBottom = Bottom.GetComponent<Renderer>().material.color;

        Top.GetComponent<Renderer>().material.color = Color.yellow;
        Bottom.GetComponent<Renderer>().material.color = Color.yellow;
     }
     void OnMouseExit()
     {
        Top.GetComponent<Renderer>().material.color = startcolorTop;
        Bottom.GetComponent<Renderer>().material.color = startcolorBottom;
     }
}
