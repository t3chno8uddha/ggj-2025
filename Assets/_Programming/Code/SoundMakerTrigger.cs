using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundMakerTrigger : MonoBehaviour
{
    [SerializeField] private bool _muteAll;
    [SerializeField] private List<SoundData> _sounds;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnSoundMakerTrigger(int audioId)
    {
        if (_muteAll) return;

        SoundData properSoundData = _sounds.Find(x => x.index == audioId);

        if (properSoundData == null) return;

        properSoundData.PlaySound(_audioSource);
    }
}


[Serializable]
public class SoundData
{
    public MonoBehaviour monoBehaviour;
    public string description;

    public AudioClip[] _sounds;

    public int index;
    public bool mute;

    public void PlaySound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(_sounds.Random());
    }
}
