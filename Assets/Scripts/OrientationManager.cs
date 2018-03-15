using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
 
public class OrientationManager : MonoBehaviour
{
 
    // This event will only be called when an orientation changed (i.e. won't be call at lanch)
    public static event UnityAction<ScreenOrientation> orientationChangedEvent;
 
    [SerializeField] private bool _debugMode = false;
 
    private ScreenOrientation _orientation;
 
    void Start()
    {
        _orientation = Screen.orientation;
        InvokeRepeating("CheckForChange", 1, 1);
    }
 
    private static void OnOrientationChanged(ScreenOrientation orientation)
    {
        if (orientationChangedEvent != null)
            orientationChangedEvent(orientation);
    }
 
    private void CheckForChange()
    {
        if (_debugMode)
            Debug.Log("Screen.orientation=" + Screen.orientation);
        if (_orientation != Screen.orientation) {
            _orientation = Screen.orientation;
            OnOrientationChanged(_orientation);
        }
    }
 
    #if UNITY_EDITOR
 
    [ContextMenu("Print Orientation")]
    private void PrintOrientation()
    {
        Debug.Log(_orientation);
    }
 
    [ContextMenu("Simulate Landscape Left")]
    private void SetLandscapeLeft()
    {
        OnOrientationChanged(ScreenOrientation.LandscapeLeft);
    }
 
    [UnityEditor.MenuItem("Debug/Orientation/Simulate Landscape Left")]
    private static void DoSetLandscapeLeft()
    {
        OnOrientationChanged(ScreenOrientation.LandscapeLeft);
    }
 
    [ContextMenu("Simulate Landscape Right")]
    private void SetLandscapeRight()
    {
        OnOrientationChanged(ScreenOrientation.LandscapeRight);
    }
 
    [UnityEditor.MenuItem("Debug/Orientation/Simulate Landscape Right")]
    private static void DoSetLandscapeRight()
    {
        OnOrientationChanged(ScreenOrientation.LandscapeRight);
    }
 
    #endif
 
}