using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControlUI : MonoBehaviour
{
    public Slider audioscroll;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = audioscroll.value;
    }
}