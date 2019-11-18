using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLogicBullets : MonoBehaviour
{
  [Header("Shooting Business")]
  [SerializeField]string shootingButton;
  [SerializeField]GameObject bulletPrefab;
  [SerializeField]GameObject bulletSpawn;
  [SerializeField]float bulletSpeed;
  [Header("CenterOfScreenAim")]
  [SerializeField]Camera fpsCam;

  void Update()
  {
    FireBullet(shootingButton);
  }

  void FireBullet(string trigger)
  {
    if(Input.GetButtonDown(trigger))
    {
      ProjectileShooting();
    }
  }
  void ProjectileShooting()
  {
    GameObject NewBullet = Object.Instantiate(bulletPrefab, bulletSpawn.transform.position, fpsCam.transform.rotation);
  }
  public void BulletMovementEquation(GameObject instanceBullet, Vector3 shootDirection)
  {
    instanceBullet.transform.position += shootDirection * bulletSpeed * Time.deltaTime;
  }
}
