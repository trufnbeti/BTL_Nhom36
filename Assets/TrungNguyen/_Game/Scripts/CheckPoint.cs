using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
    [SerializeField] private Animator anim;
    
    private bool isChecked = false;
    private const string ANIM_CHECKPOINT = "checkpoint";
    
    private void SaveCheckPoint() {
        this.PostEvent(EventID.SavePoint, transform.position);
        anim.SetTrigger(ANIM_CHECKPOINT);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(GameTag.Player.ToString())) {
            if (!isChecked) {
                isChecked = true;
                SaveCheckPoint();
            }
        }
    }
}
