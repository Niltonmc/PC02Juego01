using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using UnityEngine.SceneManagement;

public class MenuManagerControl : MonoBehaviour {

	[Header("Users Variables")]
	public int numbOfUsers;

	[Header("Input Field Variables")]
	public InputField inputFieldUserFullName;
	private string userFullName;
	public InputField inputFieldUserEmail;
	private string userEmail;
	private string userID;

	[Header("Button Variables")]
	public Button btnAddUser;
	public Button btnGoToUsers;

	void Awake(){
		FireDBControl.Init();

		FireDBControl.reference.Child("Users").ChildAdded += HandleChildAdded;
		FireDBControl.reference.Child("Users").ChildChanged += HandleChildChanged;
		FireDBControl.reference.Child("Users").ChildRemoved += HandleChildRemoved;

		btnAddUser.onClick.AddListener(() => AddUser());
		btnGoToUsers.onClick.AddListener(() => LoadUsers());

		GetUsersLength ();
	}

	void AddUser(){
		userFullName = inputFieldUserFullName.text;
		userEmail = inputFieldUserEmail.text;

		if (userFullName != "" && userEmail.Contains ("@") && userEmail.Contains (".com")) {

			userID = "NinjaGameUser" + SystemInfo.deviceUniqueIdentifier + (numbOfUsers + 1).ToString ();
			numbOfUsers = numbOfUsers + 1;

			User newUser = new User (
				               userFullName,
				               userID,
				               userEmail);
			string json = JsonUtility.ToJson (newUser);
			print (json);
			FireDBControl.reference.Child ("Users").Child (newUser.id).SetRawJsonValueAsync (json); 
			inputFieldUserFullName.text = "";
			inputFieldUserEmail.text = "";
		} else {
			print ("ERROR AL LLENAR DATOS");
		}
	}

	void GetUsersLength(){
		FirebaseDatabase.DefaultInstance
			.GetReference("Users")
			.GetValueAsync().ContinueWith(task => {
				
				numbOfUsers = (int)task.Result.ChildrenCount;
				if (task.IsFaulted) {
					print("NO HAY USUARIOS");
				}
				else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
					print("HAY: " + numbOfUsers.ToString() + " USUARIOS");
				}
			});
	}


	void Start () {

	}

	void HandleChildChanged(object sender, ChildChangedEventArgs args){
		if (args.DatabaseError != null) {
			Debug.Log (args.DatabaseError.Message);
			return;
		}
		print ("Changed " + args.Snapshot);
	}


	void HandleChildRemoved(object sender, ChildChangedEventArgs args){
		if (args.DatabaseError != null) {
			Debug.Log (args.DatabaseError.Message);
			return;
		}
		print ("Removed " + args.Snapshot);
	}

	void HandleChildAdded(object sender, ChildChangedEventArgs args){
		if (args.DatabaseError != null) {
			Debug.Log (args.DatabaseError.Message);
			return;
		}
		print ("Added " + args.Snapshot);
	}

	void LoadUsers(){
		SceneManager.LoadScene ("Users");
	}
}
