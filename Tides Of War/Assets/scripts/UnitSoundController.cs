using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSoundController : MonoBehaviour
{
    [SerializeField]
    AudioClip unitTankShoot;
    [SerializeField]
    AudioClip tankWalk;
    [SerializeField]
    AudioClip[] unitinfwalk;
    [SerializeField]
    AudioClip[] unitinfshoot;
    List<AudioSource> audioPlayers = new List<AudioSource>();
    private void Start()
    {
        audioPlayers.Add( gameObject.AddComponent<AudioSource>());
        audioPlayers.Add(gameObject.AddComponent<AudioSource>());
        audioPlayers.Add(gameObject.AddComponent<AudioSource>());
        audioPlayers.Add(gameObject.AddComponent<AudioSource>());
    }

    public void PlayTankShoot()
    {
        audioPlayers[0].clip = unitTankShoot;
        audioPlayers[0].Play();
    }

    public void PlayUnitShoot()
    {
        audioPlayers[0].clip = unitinfshoot[0];
        audioPlayers[1].clip = unitinfshoot[1];

        audioPlayers[0].Play();
        audioPlayers[1].Play();

        
    }

    public void PlayTankWalk()
    {
        audioPlayers[0].clip = tankWalk;
        audioPlayers[0].Play();
    }

    public void PlayUnitWalk()
    {
        audioPlayers[0].clip = unitinfwalk[0];
        audioPlayers[1].clip = unitinfwalk[1];
        audioPlayers[2].clip = unitinfwalk[2];
        audioPlayers[3].clip = unitinfwalk[3];
        audioPlayers[0].Play();
        audioPlayers[1].Play();
        audioPlayers[2].Play();
        audioPlayers[3].Play();

    }

}
