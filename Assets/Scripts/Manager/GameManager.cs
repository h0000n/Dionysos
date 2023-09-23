using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Camera MainCam;

    private void Awake()
    {
        MainCam = Camera.main;
    }
}
