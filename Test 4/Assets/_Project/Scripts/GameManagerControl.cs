using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerControl : MonoBehaviour {

	public static GameManagerControl instance;
	private GameObject playerTmp;

	public GameObject player;
	public GameObject platform;

	public float minX = -2.5f;
	public float maxX = 2.5f;
	public float minY = -4.7f;
	public float maxY = 4.7f;

	private bool lerpCamera;
	private float lerpTime = 1.5f;
	private float lerpX;

	public Slider powerBar;

	void Awake () {
		MakeInstace ();
		CreateInitialPlatform ();
		Initial ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MakeInstace (){
		if (instance == null) {
			instance = this;
		}
	}

	void CreateInitialPlatform (){
		Vector3 tmp = new Vector3 (Random.Range (minX, minX + 1.2f), Random.Range (minY, maxY), 0);
		Instantiate (platform, tmp, transform.rotation);

		tmp.y += 2;
		playerTmp = Instantiate (player, tmp, transform.rotation);

		tmp = new Vector3 (Random.Range (maxX, maxX - 1.2f), Random.Range (minY, maxY), 0);
		Instantiate (platform, tmp, transform.rotation);
	}

	void Initial(){
		playerTmp.GetComponent<PlayerControl> ().SetPowerBar (powerBar);
		playerTmp.GetComponent<PlayerControl> ().SetInitialValues ();

	}
}
