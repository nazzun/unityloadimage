using UnityEngine;
using System.Collections;

public class LoadImage : MonoBehaviour {

	[SerializeField]
	private string URL;
	private static LoadImage loadImageObject;
	private static bool onloadImage;

	void Start () {
		loadImageObject = this;
	}
	public static void loadSpriteToObject(string newURL, GameObject g)
	{
		if (loadImageObject == null)
			loadImageObject = new GameObject("LoadImageManager").AddComponent<LoadImage>();
		loadImageObject.loadSpriteImage(newURL, g);
	}
	private void loadSpriteImage(string newURL, GameObject g)
	{
		if (onloadImage)
			return;
		if (string.IsNullOrEmpty(newURL))
			newURL = this.URL;
		StartCoroutine(loadSpriteImageHelper(newURL, g));
	} 
	IEnumerator loadSpriteImageHelper(string newURL, GameObject g)
	{
		onloadImage = true;
		string filename = System.IO.Path.GetFileName(newURL);
		string localPATH = Application.persistentDataPath +"/" + filename;

		//IF IMAGE EXIST ON LOCAL PATH (LOAD FROM LOCAL IMAGE URL)
		if (System.IO.File.Exists(localPATH))
		{
			#if UNITY_EDITOR
			URL = "file:///" + localPATH;
			#elif UNITY_ANDROID
			URL = "file://" + localPATH;
			#endif

			var www = new WWW(newURL);
			Debug.Log("load progress from local path");
			yield return www;
			Debug.Log("load succes from loacal path");

			Texture2D texture = new Texture2D(1, 1); 				//CREATE TEXTURE WIDTH = 1 AND HEIGHT =1
			www.LoadImageIntoTexture(texture); 						//LOAD DOWNLOADED TEXTURE TO VARIABEL TEXTURE
			Sprite sprite = Sprite.Create(texture, 
				new Rect(0, 0, texture.width, texture.height),      //LOAD TEXTURE FROM POINT (0,0) TO TEXTURE(WIDTH, HEIGHT)
				Vector2.one/2);										//SET PIVOT TO CENTER

			g.GetComponent<SpriteRenderer>().sprite = sprite; 		//CHANGE CURRENT SPRITE
		}
		//IF IMAGE NOT YET DOWNLOAD (LOAD FROM WEB IMAGE URL)
		else{
			//CHECK YOUR INTERNET CONNECTION
			if (Application.internetReachability == NetworkReachability.NotReachable){
				Debug.Log("No Connection Internet");
				yield return null;
			}
			else{
				var www = new WWW(newURL);
				Debug.Log("Download image on progress");
				yield return www;

				if (string.IsNullOrEmpty(www.text))
					Debug.Log("Download failed");
				else
				{
					Debug.Log("Download Succes");

					System.IO.File.WriteAllBytes(localPATH, www.bytes);		//SAVE IMAGE TO LOCAL PATH
					Texture2D texture = new Texture2D(1, 1); 				//CREATE TEXTURE WIDTH = 1 AND HEIGHT =1
					www.LoadImageIntoTexture(texture); 						//LOAD DOWNLOADED TEXTURE TO VARIABEL TEXTURE
					Sprite sprite = Sprite.Create(texture, 
						new Rect(0, 0, texture.width, texture.height),      //LOAD TEXTURE FROM POINT (0,0) TO TEXTURE(WIDTH, HEIGHT)
						Vector2.one/2);										//SET PIVOT TO CENTER

					g.GetComponent<SpriteRenderer>().sprite = sprite; 		//CHANGE CURRENT SPRITE
				}
			}
		}
		onloadImage = false;
	}
	public static bool IsReady
	{
		get { return !onloadImage;}
	}
}
