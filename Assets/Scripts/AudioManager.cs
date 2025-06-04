using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioClip shotClip;
    [SerializeField] private AudioClip reLoadClip;
    [SerializeField] private AudioClip tankFire;
    [SerializeField] private AudioClip footstep;
    [SerializeField] private AudioSource BackgroundAudio;
    [SerializeField] private AudioSource BossAudio; 
   // [SerializeField] private AudioSource ZombieAudio;
    public void shotPlay()
    {
        effectAudioSource.PlayOneShot(shotClip);
    }
    public void reLoadPlay()
    {
        effectAudioSource.PlayOneShot(reLoadClip);
    }
    public void FootStep()
    {
        effectAudioSource.PlayOneShot(footstep);
    }
    public void TankFire()
    {
        effectAudioSource.PlayOneShot(tankFire);
    }
    public void BackgroundPlay()
    {
        BossAudio.Stop();
        BackgroundAudio.Play();
    }
    public void BossAudioPlay()
    {
        BackgroundAudio.Stop();
        BossAudio.Play();
    }
    //public void ZombiePlay()
    //{
    //    ZombieAudio.Play();
    //}
}
