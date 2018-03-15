using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExperienceLauncher : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("teste porra");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadBundleScene()
	{
		PlayerPrefs.SetString("SceneAssetBundle", "shn_bundle");
		PlayerPrefs.SetString("SceneName", "scene_01_shn");
		SceneManager.LoadSceneAsync(1);
	}
}
