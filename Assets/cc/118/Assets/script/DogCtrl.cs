using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCtrl : MonoBehaviour
{
    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move_Update();
    }

    private void Move_Update()
    {
        if(Input.GeyKey(KeyCode.W))
        {
            transform.Translate(Vector.forward * speed * Time.deltaTime);
        }
        if (Input.GeyKey(KeyCode.S))
        {
            transform.Translate(Vector.back * speed * Time.deltaTime);
        }
        if (Input.GeyKey(KeyCode.A))
        {
            transform.Translate(Vector.left * speed * Time.deltaTime);
        }
        if (Input.GeyKey(KeyCode.D))
        {
            transform.Translate(Vector.right * speed * Time.deltaTime);
        }
    }
}
