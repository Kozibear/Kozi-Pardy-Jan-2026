using UnityEngine;
using System.Collections;

public class RightWrongSFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip rightSFX;
    [SerializeField] AudioClip wrongSFX;

    [SerializeField] float waitBeforeStoppingRightAudio = 1;
    [SerializeField] float waitBeforeStoppingWrongAudio = 1;

    [SerializeField] float rightVolume = 1;
    [SerializeField] float wrongVolume = 1;

    private bool currentlyPlaying = false;

    public void PlayRight()
    {
        if (!currentlyPlaying)
        {
            currentlyPlaying = true;
            audioSource.clip = rightSFX;
            audioSource.volume = rightVolume;
            StartCoroutine(PlaySFX(waitBeforeStoppingRightAudio));
        }
    }

    public void PlayWrong()
    {
        if (!currentlyPlaying)
        {
            currentlyPlaying = true;
            audioSource.clip = wrongSFX;
            audioSource.volume = wrongVolume;
            StartCoroutine(PlaySFX(waitBeforeStoppingWrongAudio));
        }
    }

    private IEnumerator PlaySFX(float wait)
    {
        audioSource.Play();
        yield return new WaitForSeconds(wait);
        audioSource.Stop();
        currentlyPlaying = false;
    }

}
