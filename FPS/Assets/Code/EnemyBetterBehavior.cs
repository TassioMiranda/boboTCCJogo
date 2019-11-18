using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBetterBehavior : MonoBehaviour
{
  [Header("References")]
  [SerializeField] GameObject enemyBullet;
  [SerializeField] GameObject playerReference;
  [SerializeField] GameObject enemyBulletSpawn;

  [Header("Movement")]
  [SerializeField] float movementSpeed;
  bool isChasing;

  [Header ("Shooting")]
  [SerializeField]float shootTimerInit;
  [SerializeField]float shootTimer;
  bool isShooting;

  [Header ("Knockback State")]
  [SerializeField] float knockbackForce;
  [SerializeField] float knockbackTimerInit;
  float knockbackTween;
  [SerializeField] AnimationCurve knockbackCurve;
  float knockbackTimer;
  Vector3 knockbackDirection;
  bool isKnockback;

  bool playerInSight;

  void Start()
  {
    playerReference = GameObject.FindWithTag("Player");
    playerInSight = true;
    isChasing = true;
    isShooting = true;
    shootTimer = shootTimerInit;
    knockbackTimer = knockbackTimerInit;
  }

  void Update()
  {
    shootTimer -= Time.deltaTime;
    if(playerInSight){
      if(isChasing)
      {
        chaseLogic();
      }
      if(isShooting && shootTimer <=0)
      {
        shootLogic();
        shootTimer = shootTimerInit;
      }
      if(isKnockback == true && knockbackTimer >= .0f)
      {
        knockbackState();
      }
      else
      {
        knockbackTimer = knockbackTimerInit;
        knockbackTween = .0f;
        isKnockback = false;
        knockbackTimer = knockbackTimerInit;
      }
    }
  }

  void chaseLogic()
  {
    this.transform.LookAt(playerReference.transform);
    //this.transform.position += this.transform.forward * movementSpeed * Time.deltaTime;
  }
  void shootLogic()
  {
    //this.transform.LookAt(playerReference.transform);
    GameObject newEnemyBullet = Object.Instantiate(enemyBullet, enemyBulletSpawn.transform.position, enemyBulletSpawn.transform.rotation);
  }
  void knockbackState()
  {
    float knockbackFallout = knockbackCurve.Evaluate(knockbackTween);
    this.transform.position -= knockbackDirection * knockbackForce * knockbackFallout * Time.deltaTime;
    knockbackTween += Time.deltaTime;
    knockbackTimer -= Time.deltaTime;
  }
  public void SetKnockbackStateDirection(Vector3 newKnockbackDirection)
  {
    knockbackDirection = newKnockbackDirection;
  }
  public void SetKnockbackState()
  {
    isKnockback = true;
  }
}
