using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MessageScript : MonoBehaviour {
public static string Message ="";
	Text MessageText;
	// Use this for initialization
	void Start () {
		MessageText = GetComponent<Text>();
		Message ="";
	}
	
	// Update is called once per frame
	void Update () {
		MessageText.text = Message;
	}
}
