using UnityEngine;
using System;
[Serializable]
public struct AudioSettings
{
    [Header("Background audio")]
    public AudioClip mainMenu;
    public AudioClip easyGame;
    public AudioClip mediumGame;
    public AudioClip hardGame;
    public AudioClip gameOver;

    [Header("Game audio")]
    public AudioClip truckWheels;
    public AudioClip construction;
    public AudioClip click;
}
