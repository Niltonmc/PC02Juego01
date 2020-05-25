using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

	public static PlayerControl instance;
	private Rigidbody2D rbPlayer;
	private Animator animPlayer;

	public float forceX;
	public float forceY;

	private float tresHoldx = 7f;
	private float tresHoldY = 14f;

	private bool setPower, didJump;

	private float maxForceX = 6.5f;
	private float maxForceY = 13.5f;

	private Slider powerBar;
	private float powerBarTresHold = 10f;
	private float powerBarValue = 0f;

	void Awake(){
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SetPower ();
	}

	void MakeInstance(){
		if (instance == null) {
			instance = this;
		}
	}

	void Init(){
		rbPlayer = GetComponent<Rigidbody2D> ();
		animPlayer = GetComponent<Animator> ();

		powerBar.minValue = powerBarValue;
		powerBar.maxValue = powerBarTresHold;
		powerBar.value = powerBarValue;
	}

	void SetPower(){
		if (setPower) {
			forceX += tresHoldx * Time.deltaTime;
			forceY += tresHoldY * Time.deltaTime;
		

			if (forceX > maxForceX) {
				forceX = maxForceX;
			}

			if (forceY > maxForceY) {
				forceY = maxForceY;
			}
			powerBarValue += powerBarTresHold * Time.deltaTime;
			powerBar.value = powerBarValue;
		}
	}

	public void GivePower(bool power){
		setPower = power;
		if (!setPower) {
			Jump();
		}
	}

	void Jump(){
		rbPlayer.velocity = new Vector2 (forceX, forceY);
		forceX = forceY = 0;
		didJump = true;
		animPlayer.SetBool ("isJumping", true);
		powerBarValue = 0;
		powerBar.value = powerBarValue;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (didJump) {
			didJump = false;
			animPlayer.SetBool ("isJumping", didJump);
		}
		if (other.gameObject.tag == "Platform") {
			
		}
	}

	public void SetPowerBar (Slider tmp){
		powerBar = tmp;
	}

	public void SetInitialValues(){
		MakeInstance ();
		Init ();
	}
}
