using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
    }
}
