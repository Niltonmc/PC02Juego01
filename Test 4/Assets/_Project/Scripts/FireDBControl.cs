using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FireDBControl {

	private static string url = "https://dj01proyectoninja.firebaseio.com/";
	public static DatabaseReference reference;

	public static void Init(){
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl (url);
		reference = FirebaseDatabase.DefaultInstance.RootReference;
	}
}
