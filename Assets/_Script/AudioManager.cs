using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonobehavior<AudioManager>
{
    [SerializeField]
    private AudioSource BGM_AudioSource;
    [SerializeField]
    private AudioSource SFX_AudioSource;
    [SerializeField]
    private AudioSource Particle_AudioSource;
    public void PlaySound(AudioClip clip)
    {
        SFX_AudioSource.PlayOneShot(clip);
    }
    public void PlayParticleFX()
    {
        Particle_AudioSource.Play();
    }
    public void PlayBGM(AudioClip clip)
    {
        BGM_AudioSource.Stop();
        BGM_AudioSource.clip = clip;
        BGM_AudioSource.Play();
    }
    public void ToggleBGM()
    {
        BGM_AudioSource.mute = !BGM_AudioSource.mute;
    }
    public void ToggleSFX()
    {
        SFX_AudioSource.mute = !SFX_AudioSource.mute;
        Particle_AudioSource.mute = !Particle_AudioSource.mute;
    }
    public void ChangeVolume(float volume)
    {
        BGM_AudioSource.volume = volume;
        SFX_AudioSource.volume = volume;
        Particle_AudioSource.volume = volume;
    }

}
