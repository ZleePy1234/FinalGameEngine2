using UnityEngine;

public class PistolSounds : MonoBehaviour
{
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public AudioSource audioSource;
    public ParticleSystem muzzleFlash;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FireSFX()
    {
        audioSource.PlayOneShot(fireSound);
        muzzleFlash.Play();
    }

    void ReloadSFX()
    {
        audioSource.PlayOneShot(reloadSound);
    }
}
