using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLogicRaycast : MonoBehaviour
{
  [SerializeField][Range(1f, 1000f)] float shootingRange;
  [SerializeField] string shootingButton;
  public float knockbackForce;
  public AnimationCurve knockbackCurve;
  public RaycastHit hit;

  public Camera fpsCam;

  void Update()
  {
    FireGun(shootingButton);
  }
  void FireGun(string shoot)
  {
    if(Input.GetButtonDown(shoot))
    {
      RayCastShooting();
    }
  }
  void RayCastShooting()
  {
    if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, shootingRange))
    {
      if(hit.transform.tag == "Enemy")
      {
        EnemyBehavior _enemyScript = hit.transform.gameObject.GetComponent<EnemyBehavior>();
        _enemyScript.SetKnockbackState();
        Debug.Log(hit.transform.name);
      }
    }
  }
}
