using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [SerializeField] private int audioSourcePoolSize = 4;

    public static AudioManager Instance { get; private set; }
    private AudioSource[] audioSources;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Debug.LogWarning("AudioManager instance recreated");
            Destroy(this);
            return;
        }
        Instance = this;

        audioSources = new AudioSource[audioSourcePoolSize];
        for (int i = 0; i < audioSources.Length; ++i) {
            GameObject newGameObject = new("AudioSource");
            newGameObject.transform.SetParent(transform);
            audioSources[i] = newGameObject.AddComponent<AudioSource>();
        }
    }

    private void Start() {
        DeliveryCounter.OnDeliveryFailed += HandleDeliveryFailed;
        DeliveryCounter.OnDeliverySuccess += HandleDeliverySuccess;
        CuttingCounter.OnAnyCut += HandleAnyCut;
        Player.Instance.OnPickupSomething += HandlePlayerPickupSomething;
        ClearCounter.OnObjectPlacedOn += HandleObjectPlacedOnClearCounter;
        CuttingCounter.OnObjectPlacedOn += HandleObjectPlacedOnCuttingCounter;
        StoveCounter.OnObjectPlacedOn += HandleObjectPlacedOnStoveCounter;
        TrashCounter.OnTrash += HandleTrash;
    }

    public void Play(AudioClip audioClip, Vector3 position, float volume = 1) {
        var freeSource = GetFreeAudioSource();
        if (!freeSource) {
            Debug.LogWarning("[AudioManager] Failed to play audio: ", audioClip);
        }
        freeSource.clip = audioClip;
        freeSource.transform.position = position;
        freeSource.volume = volume;
        freeSource.Play();
    }

    public void Play(AudioClip[] audioClips, Vector3 position, float volume = 1) {
        Play(audioClips[Random.Range(0, audioClips.Length)], position, volume);
    }

    private AudioSource GetFreeAudioSource() {
        foreach (AudioSource audiosource in audioSources) {
            if (!audiosource.isPlaying) {
                return audiosource;
            }
        }
        Debug.LogWarning("[AudioManager] No free AudioSource at the time");
        return null;
    }

    private void HandleDeliverySuccess(Vector3 position) {
        Play(audioClipRefsSO.deliverSuccess, position);
    }

    private void HandleDeliveryFailed(Vector3 position) {
        Play(audioClipRefsSO.deliveryFail, position);
    }

    private void HandleAnyCut(Vector3 position) {
        Play(audioClipRefsSO.chop, position);
    }

    private void HandlePlayerPickupSomething(Vector3 position) {
        Play(audioClipRefsSO.objectPickup, position);
    }

    private void HandleObjectPlacedOnClearCounter(Vector3 position) {
        Play(audioClipRefsSO.objectDrop, position);
    }

    private void HandleObjectPlacedOnCuttingCounter(Vector3 position) {
        Play(audioClipRefsSO.objectDrop, position);
    }

    private void HandleObjectPlacedOnStoveCounter(Vector3 position) {
        Play(audioClipRefsSO.objectDrop, position);
    }

    private void HandleTrash(Vector3 position) {
        Play(audioClipRefsSO.trash, position);
    }

    public void PlayFootstep(Vector3 position, float volume) {
        Play(audioClipRefsSO.footstep, position, volume);
    }
}
