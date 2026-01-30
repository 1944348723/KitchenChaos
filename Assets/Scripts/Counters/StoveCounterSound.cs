using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += HandleStateChanged;
    }

    private void HandleStateChanged(StoveCounter.StoveState stoveState) {
        if (stoveState == StoveCounter.StoveState.Frying || stoveState == StoveCounter.StoveState.Burning) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
    }
}
