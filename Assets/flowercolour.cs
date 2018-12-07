using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowercolour : MonoBehaviour {

    public Gradient colourGradient;
    public float variationAmount;
    public Material baseMat;
    private Material mat;
    
	// Use this for initialization
	void Start () {
        mat = new Material(baseMat);
        float colourStep = Random.Range(0, variationAmount) / variationAmount;
        print(colourStep);
        mat.SetColor("_Color", colourGradient.Evaluate(colourStep));
        this.gameObject.GetComponent<MeshRenderer>().material = mat;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
