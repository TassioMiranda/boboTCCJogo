using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSlap : MonoBehaviour
{
  [SerializeField]GameObject gun;
  [SerializeField]float bulletLifetime;
  Vector3 mPreviousPos;
  public float bulletSpeed;
  Vector3 bulletDirection;
  void Start()
  {
    gun = GameObject.FindWithTag("Weapon");
    mPreviousPos = this.transform.position;
    bulletSpeed = bulletSpeed;
    bulletDirection = this.transform.forward;
  }
  void Update()
  {
    mPreviousPos = this.transform.position;
    bulletLifetime -= Time.deltaTime;
    if(bulletLifetime <= 0)
    {
      Object.Destroy(this.gameObject);
    }
    this.transform.position += bulletDirection * bulletSpeed * Time.deltaTime;

    RaycastHit[] hits = Physics.RaycastAll(new Ray(mPreviousPos,(this.transform.position - mPreviousPos).normalized),(this.transform.position - mPreviousPos).magnitude);

    for(int i = 0; i < hits.Length; i++)
    {
      if(hits[i].collider.gameObject.name == "Enemy")
      {
        Debug.Log(hits[i].collider.gameObject.name);
        Object.Destroy(this.transform.gameObject);
        hits[i].collider.gameObject.GetComponent<EnemyBetterBehavior>().SetKnockbackStateDirection(hits[i].normal);
        hits[i].collider.gameObject.GetComponent<EnemyBetterBehavior>().SetKnockbackState();

        if(hits[i].collider.gameObject.CompareTag("Weapon"))
        {
          bulletSpeed = -bulletSpeed;
          Debug.Log("HitBullet");
        }
      }
    }
    Debug.DrawLine(this.transform.position, mPreviousPos);
  }
}
