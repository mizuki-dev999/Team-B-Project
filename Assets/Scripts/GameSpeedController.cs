using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    public void OnStopButton() => Time.timeScale = 0;
    public void OnSlowButton() => Time.timeScale = 0.4f;
    public void OnRsetSpeedButton() => Time.timeScale = 1;
}
