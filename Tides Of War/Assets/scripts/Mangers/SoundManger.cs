using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    
    public UnitSoundController UnitSoundController;
    public static SoundManger Instance { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

}
