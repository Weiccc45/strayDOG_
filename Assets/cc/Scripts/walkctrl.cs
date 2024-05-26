using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkctrl : MonoBehaviour
{

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            position.x += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            position.y += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            position.y -= speed * Time.deltaTime;
        }

        transform.position = position;
    }
}
