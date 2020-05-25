using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown(PointerEventData eventData){
		if (PlayerControl.instance != null) {
			PlayerControl.instance.GivePower (true);
		}
	}

	public void OnPointerUp (PointerEventData eventData){
		if (PlayerControl.instance != null) {
			PlayerControl.instance.GivePower (false);
		}
	}
}
