using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Slider volumeSlider;


    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
