using UnityEngine;

public class BossTheme : MonoBehaviour
{
    private AudioManager AudioManager;
    void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
       // BossAudio();
    }
    public void BossAudio()
    {
        AudioManager.BossAudioPlay();
    }
    
}
