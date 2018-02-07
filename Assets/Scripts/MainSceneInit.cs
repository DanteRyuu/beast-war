using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainSceneInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SoundManager.PlayAmbientalSound(GetComponentInChildren<AudioSource>(), 0);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
