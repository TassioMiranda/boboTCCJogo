using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletGraphics : MonoBehaviour
{
  public float spinSpeed;

  void Update()
  {
    this.transform.Rotate(0f,0f,spinSpeed);
  }
}
