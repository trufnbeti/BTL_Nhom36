using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float parallaxEffect;
    
    private GameObject cam;
    private float startPos, length;
    private float temp, dist;
    private Vector3 newPos;
    
    private void Start() {
        cam = GameManager.Ins.cam.gameObject;
        startPos = transform.position.x;
        length = spriteRenderer.bounds.size.x;
    }

    private void LateUpdate() {
        temp = cam.transform.position.x * (1 - parallaxEffect);
        dist = cam.transform.position.x * parallaxEffect;
        newPos.Set(startPos + dist, cam.transform.position.y - 1f, transform.position.z);

        transform.position = newPos;

        if (temp > startPos + length) {
            startPos += length;
        } else if (temp < startPos - length) {
            startPos -= length;
        }
    }
}
