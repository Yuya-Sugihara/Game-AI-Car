using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Carの操作に関するflagを管理する。

public class InputForCar : MonoBehaviour {

	public bool wFlag=false;
	public bool aFlag=false;
	public bool dFlag=false;
	public bool sFlag=false;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("w")) wFlag=true;
		if(Input.GetKeyDown("a")) aFlag=true;
		if(Input.GetKeyDown("d")) dFlag=true;
		if(Input.GetKeyDown("s")) sFlag=true;

		if(Input.GetKeyUp("w")) wFlag=false;
		if(Input.GetKeyUp("a")) aFlag=false;
		if(Input.GetKeyUp("d")) dFlag=false;
		if(Input.GetKeyUp("s")) sFlag=false;
	}
}
