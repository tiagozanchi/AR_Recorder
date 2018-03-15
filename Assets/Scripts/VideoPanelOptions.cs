using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;

public class VideoPanelOptions : MonoBehaviour {

	public VideoPlayer video;
	public RawImage videoImage;
	public Texture videoTex;
	public Image playPauseButton;
	public Button recButton;
	private Animator anim;
	private bool playingVideo = false;


	// Use this for initialization
	void Start () {
		Everyplay.RecordingStopped += OnRecordingStopped;
		anim = GetComponent<Animator>();
	}

	void OnDestroy()
	{
		Everyplay.RecordingStopped -= OnRecordingStopped;
	}


	private void OnRecordingStopped()
	{
		Vuforia.VuforiaBehaviour.Instance.enabled = false;
		StartCoroutine(ShowRecordedVideo());
	}

	IEnumerator ShowRecordedVideo()
	{
		// Only async on iOS, results immediately on Android
		string tempPath = Path.Combine( Application.persistentDataPath, "boom_real_temp.mp4" );
		yield return EveryplayLocalSave.SaveToAsync( tempPath );

		video.url = tempPath;
		
		PlayPauseVideo();
		anim.SetBool("Show", true);
	}

	public void PlayPauseVideo()
	{
		if(playingVideo)
		{
			playPauseButton.enabled = true;
			video.Pause();
		}
		else
		{
			if(videoImage.texture != videoTex)
			{
				videoImage.texture = videoTex;
			}
			playPauseButton.enabled = false;
			video.Play();
		}
		playingVideo = !playingVideo;
	}

	public void DiscartVideo()
	{
		File.Delete(Path.Combine( Application.persistentDataPath, "boom_real_temp.mp4" ));
		HideAndEnableRec();
	}

	public void SaveVideo()
	{
		NativeGallery.SaveVideoToGallery(Path.Combine( Application.persistentDataPath, "boom_real_temp.mp4" ), "BoomReal", "Boom Real {0}.mp4");
		HideAndEnableRec();
	}

	public void Share()
	{
		new NativeShare().AddFile( Path.Combine( Application.persistentDataPath, "boom_real_temp.mp4" ) ).SetTitle( "COMPARTILHAR" ).SetText( "Hello world!" ).Share();
		SaveVideo();
		HideAndEnableRec();
	}

	void HideAndEnableRec()
	{
		playingVideo = false;
		Vuforia.VuforiaBehaviour.Instance.enabled = true;
		anim.SetBool("Show",false);
		recButton.interactable = true;
		playPauseButton.enabled = true;
	}
}
