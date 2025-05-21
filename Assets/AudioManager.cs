using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioClip shotClip;
    [SerializeField] private AudioClip reLoadClip;
    [SerializeField] private AudioClip footstep;
    
    public void shotPlay()
    {
        effectAudioSource.PlayOneShot(shotClip);
    }
    public void reLoadPlay()
    {
        effectAudioSource.PlayOneShot(reLoadClip);
    }
    //public void FootStep()
    //{
    //    effectAudioSource.PlayOneShot(footstep);
    //}
}
