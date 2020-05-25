using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class FireBaseExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FireDBControl.Init();
		User user = new User("Luis",
			SystemInfo.deviceUniqueIdentifier,
			"valdivialuis1989@gmail.com");

		string json = JsonUtility.ToJson(user);
		print(json);

		//FireDBControl.reference.Child("users").Child(user.id).SetRawJsonValueAsync(json); 
	
		FireDBControl.reference.Child("users").ChildAdded += HandleChildAdded;
		FireDBControl.reference.Child("users").ChildChanged += HandleChildChanged;
		FireDBControl.reference.Child("users").ChildRemoved += HandleChildRemoved;
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
}
