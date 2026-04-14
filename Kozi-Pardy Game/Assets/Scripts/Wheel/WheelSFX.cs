using UnityEngine;
using System.Collections;

public class WheelSFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [Header("Segment Ding")]
    [SerializeField] AudioClip segmentDing;
    [SerializeField] float dingVolume = 1;
    [SerializeField] float dingPitch = 1;
    [SerializeField] float minimumWaitBeforeNextDing = 0f;
    private bool pitchingUp = true;

    [Header("End Chime")]
    [SerializeField] AudioClip endChime;
    [SerializeField] float chimeVolume = 1;
    [SerializeField] float chimePitch = 1;

    [SerializeField] float waitBeforeStoppingEndChime = 1;

    private bool currentlyPlaying = false;
    private bool currentlyPlayingEndChime = false;

    private float timeElapsed = 0;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    public void PlaySegmentDing()
    {
        if (timeElapsed < minimumWaitBeforeNextDing) return;
        timeElapsed = 0;
        
        if (!currentlyPlayingEndChime)
        {
            audioSource.clip = segmentDing;
            audioSource.volume = dingVolume;

            audioSource.pitch = dingPitch;

            StartCoroutine(PlaySFX(0, false));
        }
    }

    public void PlayEndChime()
    {
        currentlyPlayingEndChime = true;

        audioSource.clip = endChime;
        audioSource.volume = chimeVolume;
        audioSource.pitch = chimePitch;

        StartCoroutine(PlaySFX(waitBeforeStoppingEndChime, true));
    }

    private IEnumerator PlaySFX(float wait, bool isEndChime)
    {
        if (currentlyPlaying) audioSource.Stop();
        currentlyPlaying = true;

        audioSource.Play();

        yield return new WaitForSeconds(wait);

        if (isEndChime)
        {
            audioSource.Stop();
            currentlyPlayingEndChime = false;
        }
    }

    private void PitchChanging()
    {
        if (pitchingUp)
        {
            dingPitch += 0.1f;
            if (dingPitch >= 1.0f) pitchingUp = false;
        }
        else
        {
            dingPitch -= 0.1f;
            if (dingPitch <= 0.4f) pitchingUp = true;
        }
    }

    public float GetTimeElapsed()
    {
        return timeElapsed;
    }
}
