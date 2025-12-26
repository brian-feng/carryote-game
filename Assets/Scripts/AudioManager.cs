using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip RollSoundEffect;
    [SerializeField] private AudioSource Source;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayRollSoundEffect()
    {
        Source.PlayOneShot(RollSoundEffect);
    }
}
