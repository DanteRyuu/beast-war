using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessagesScript : MonoBehaviour {

    public Text textBox;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetText(string text)
    {
        textBox.text = text;
        GameManager.Get().StartCoroutine(ClearText(3));
    }

    private IEnumerator ClearText(float delay)
    {
        yield return new WaitForSeconds(delay);
        textBox.text = "";
    }
}
