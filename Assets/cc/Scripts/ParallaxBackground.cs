using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform player;
    public float parallaxEffectMultiplier = 0.5f;

    private Vector3 lastPlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned.");
            return;
        }

        lastPlayerPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector3 deltaMovement = player.position - lastPlayerPosition;
        float backgroundVerticalSpeed = deltaMovement.y * parallaxEffectMultiplier;

        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, backgroundVerticalSpeed, 0);
        
        lastPlayerPosition = player.position;
    }
}
