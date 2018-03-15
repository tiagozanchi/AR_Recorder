using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadBundleScene : MonoBehaviour
{
	private string sceneAssetBundle;
	private string sceneName;
	
	void Start () {
    	StartCoroutine(StartCo());  
  	}

	// Use this for initialization
	IEnumerator StartCo ()
	{
		AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, PlayerPrefs.GetString("SceneAssetBundle")));
		
		AssetBundleRequest request = myLoadedAssetBundle.LoadAssetAsync("ImageTarget", typeof(GameObject));
		yield return request;

		Debug.Log ("loaded asset: " + request.asset.ToString());
		GameObject prefab = request.asset as GameObject;

		GameObject go = (GameObject)Instantiate(prefab);
		
		AssetBundleRequest vuforia_database = myLoadedAssetBundle.LoadAssetAsync("Boom_Real.xml");
		yield return vuforia_database;
		
		myLoadedAssetBundle.Unload(false);
		Vuforia.DatabaseLoadARController.Instance.LoadDatasets();
	}
}
