using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator PlayerObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z)) {
            PlayerObj.SetBool("Att", true);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            PlayerObj.SetBool("Att", false);

        }
        if (Input.GetKey(KeyCode.X))
        {
            PlayerObj.SetBool("Smell", true);

        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            PlayerObj.SetBool("Smell", false);

        }

    }
}
