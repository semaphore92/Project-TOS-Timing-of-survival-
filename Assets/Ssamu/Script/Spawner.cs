using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    void Update() {
        if(Input.GetButtonDown("Jump")){
            GameManager.instance.pool.Get(0);
        }
    }
}
