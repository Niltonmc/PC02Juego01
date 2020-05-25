using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserContainerControl : MonoBehaviour {

	[Header("Texts Variables")]
	public Text txtUserName;
	public Text txtUserEmail;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetUserVariables(string name, string email){
		txtUserName.text = name;
		txtUserEmail.text = email;
	}
}
