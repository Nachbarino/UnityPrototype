using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench_OpenHighlight : MonoBehaviour
{
    private GameObject chest;
    private GameObject Top;
    private GameObject Bottom;

    public Material visibleThroughWallsMaterial;
    private Material originalMaterial;

    void Start()
    {
        originalMaterial = transform.GetComponent<MeshRenderer>().material;    
    }
    void OnMouseEnter()
    {
        transform.GetComponent<MeshRenderer>().material= visibleThroughWallsMaterial;
    }
    void OnMouseExit()
    {
        transform.GetComponent<Renderer>().material = originalMaterial;
    }
}
