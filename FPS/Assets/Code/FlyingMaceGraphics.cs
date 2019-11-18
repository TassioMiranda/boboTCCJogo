using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMaceGraphics : MonoBehaviour
{
    public float rotateSpeed;
    void Update()
    {
      this.transform.Rotate(0f,0f,0f);
    }
}
