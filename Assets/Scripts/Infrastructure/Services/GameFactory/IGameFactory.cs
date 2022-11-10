﻿using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory
{
    List<ISavableProgressReader> ProgressReaders { get; }
    List<ISavableProgress> ProgressWriters { get; }
    
    Player CreatePlayerCharacter(Vector3 position);
    CameraController CreateCameraController();
    HUD CreateHUD();
    UITradingPanel CreateTradingPanel(string path);
    void Cleanup();
}