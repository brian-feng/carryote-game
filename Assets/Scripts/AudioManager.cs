using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip RollSoundEffect;
    [SerializeField] private AudioClip BuyXPSoundEffect; 
    [SerializeField] private AudioClip BuyUnitSoundEffect;
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

    public void PlayBuyXPSoundEffect()
    {
        Source.PlayOneShot(BuyXPSoundEffect);
    }

    public void PlayBuyUnitSoundEffect()
    {
        Source.PlayOneShot(BuyUnitSoundEffect);
    }
}
