using UnityEngine;

namespace RPG.Audio
{
    public class RandomAudio : MonoBehaviour
    {
        [SerializeField] AudioClip[] clips = null;
        [SerializeField] float lowerPitchRange = 0.25f;
        [SerializeField] float higherPitchRange = 0.8f;

        AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        public void PlayAudio()
        {
            if (source == null || clips.Length < 1) return;

            RandomiseClip();
            RandomisePitch();

            source.Play();
        }

        private void RandomisePitch()
        {
            source.pitch = Random.Range(lowerPitchRange, higherPitchRange);
        }

        private void RandomiseClip()
        {
            AudioClip clip = clips[Random.Range(0, (clips.Length - 1))];
            source.clip = clip;
        }
    }
}
