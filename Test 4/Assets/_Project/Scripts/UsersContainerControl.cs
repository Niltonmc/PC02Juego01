using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UsersContainerControl : MonoBehaviour {

	[Header("Users Variables")]
	public int numbOfUsers;
	public GameObject userPref;
	public List<UserContainerControl> allUsersContainer;

	[Header("Position Variables")]
	public Vector3 userInfoPosition;
	private float initialPosY = 190;
	private float currentPosY;
	private float minPosY = -260;

	private float initialPosX = 0;
	private float currentPosX;
	public float currentGroupX = 0;

	[Header("Button Variables")]
	public Button btnReturnMenu;
	public Button btnLeftArrow;
	public Button btnRightArrow;

	// Use this for initialization
	void Awake () {
		currentPosY = initialPosY;
		currentPosX = initialPosX;

		CreateUsers ();
		FireDBControl.Init();

		btnReturnMenu.onClick.AddListener(() => LoadMenu());
		btnLeftArrow.onClick.AddListener(() => PreviousUsers());
		btnRightArrow.onClick.AddListener(() => NextUsers());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateUsers(){
		FirebaseDatabase.DefaultInstance
			.GetReference ("Users")
			.GetValueAsync ().ContinueWith (task => {
			if (task.IsFaulted) {
				// Handle the error...
			} else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				List<string> userInfo = new List<string> ();
				foreach (var users in snapshot.Children) {
					userInfo.Clear ();
					Debug.LogFormat ("Key = {0}", users.Key);
					foreach (var userData in users.Children) {
						userInfo.Add (userData.Value.ToString ());
						//print(userData.Value.ToString());
						//Debug.LogFormat("Key = {0}, Value = {1}", userData.Key, userData.Value.ToString());
					}

					InstantiateUser (userInfo);
					currentPosY = currentPosY - 90;
					if (currentPosY < minPosY) {
						currentPosY = initialPosY;
							currentPosX = currentPosX + 500;
					}
				}
			}
		});
	}

	void InstantiateUser(List<string> info){
		userInfoPosition = new Vector3 (currentPosX, currentPosY, 0);
		GameObject user = Instantiate (userPref, userInfoPosition, transform.rotation);
		user.GetComponent<UserContainerControl> ().SetUserVariables (info [1], info [0]);
		user.transform.SetParent (this.transform);
		user.transform.localPosition = userInfoPosition;
		user.transform.localScale = new Vector3 (1, 1, 1);
		allUsersContainer.Add (user.GetComponent<UserContainerControl> ());
	}

	void LoadMenu(){
		SceneManager.LoadScene ("Menu");
	}

	void NextUsers(){
		if (currentGroupX < currentPosX) {
			for (int i = 0; i < allUsersContainer.Count; ++i) {
				allUsersContainer [i].transform.localPosition = new Vector2 (
					allUsersContainer [i].transform.localPosition.x - 500,
					allUsersContainer [i].transform.localPosition.y);
			}
			currentGroupX = currentGroupX + 500;
		}
	}

	void PreviousUsers(){
		if (currentGroupX > 0) {
			for (int i = 0; i < allUsersContainer.Count; ++i) {
				allUsersContainer [i].transform.localPosition = new Vector2 (
					allUsersContainer [i].transform.localPosition.x + 500,
					allUsersContainer [i].transform.localPosition.y);
			}
			currentGroupX = currentGroupX - 500;
		}
	}
}
