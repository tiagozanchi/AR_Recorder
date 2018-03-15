using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RecordVideo : MonoBehaviour {

	public SpriteRenderer recImage;
	public SpriteRenderer stopImage;
	public Button recButton;
	public Text debug;
	private bool rec = false;
	private string lastVideoPath;

	private Animator recAnim;

	private Vector3 touchBegin;
	private Vector3 touchEnded;

	private float minSwipe;

	void Start()
	{
		minSwipe = Screen.width / 5;
		recAnim = GetComponent<Animator>();
		Everyplay.ReadyForRecording += ActivateRec;
	}

	void OnDestroy()
	{
		Everyplay.ReadyForRecording -= ActivateRec;
	}

	// Update is called once per frame
	void Update () {

		//Debug.Log("ACTIVATE REC " + enabled + " Record Supported " + Everyplay.IsRecordingSupported());

		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			touchBegin = Input.GetTouch(0).position;
		}

		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			touchEnded = Input.GetTouch(0).position;
			CheckSwipe();
		}

	}

	public void Rec()
	{
		if(rec)
		{
			//stop rec
			recButton.interactable = false;
			recButton.image.sprite = recImage.sprite;
			Everyplay.StopRecording();
			rec = !rec;
		}
		else if(Everyplay.IsReadyForRecording() && !rec)
		{
			//start rec
			recButton.image.sprite = stopImage.sprite;
			Everyplay.StartRecording();
			rec = !rec;
		}
	}

	public void ActivateRec(bool enabled)
	{
		if(enabled) 
		{
			recButton.interactable = true;
			recButton.image.enabled = true;
		}
		else
		{
			recButton.interactable = false;
			recButton.image.enabled = false;
		}
	}

	void CheckSwipe()
	{
		float swipeVal = touchBegin.y - touchEnded.y;

		if(swipeVal < 0 && Mathf.Abs(swipeVal) > minSwipe)
		{
			recAnim.SetBool("Show", true);
		}
		else if(swipeVal > 0 && Mathf.Abs(swipeVal) > minSwipe)
		{
			recAnim.SetBool("Show", false);
		}
	}
}
