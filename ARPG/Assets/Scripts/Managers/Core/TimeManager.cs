using System;
using UnityEngine;

public class TimeManager
{
    public float DeltaTime { get { return Time.deltaTime * GameSpeed; } }

    public float GameSpeed = 1.0f;      //게임 속도 배율

    private TimeSpan _clientTime;

    public void Initialized()
    {
        _clientTime = new TimeSpan();
    }
}