using System.Collections;
using UnityEngine;

public class Demo : MonoBehaviour {

	[SerializeField]
	private string URL;

	void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width/3 - 30, Screen.height - 30, 150, 20), "Load Sprite From URL"))
		{
			if (LoadImage.IsReady)
				LoadImage.loadSpriteToObject(URL, gameObject);
		}
	}
}
