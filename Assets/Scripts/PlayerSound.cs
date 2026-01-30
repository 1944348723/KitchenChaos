using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private Player player;

    private float FOOTSTEP_INTERVAL = 0.1f;
    private float footstepTimer = 0;

    private void Update() {
        footstepTimer += Time.deltaTime;
        if (footstepTimer >= FOOTSTEP_INTERVAL) {
            footstepTimer = 0;

            if (player.IsWalking()) {
                float volume = 0.5f;
                AudioManager.Instance.PlayFootstep(transform.position, volume);
            }
        }
    }
}
