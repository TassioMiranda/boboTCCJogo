using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
  [SerializeField]GameObject mace;
  [SerializeField]GameObject player;
  [SerializeField]float walkSpeed;
  [SerializeField]float knockbackTimerInit;
  [SerializeField]AnimationCurve knockbackCurve;
  [SerializeField] float knockbackForce;
  float knockbackTimer;
  float knockbackTween = .0f;
  public bool beingKnockback;
  public bool isChasing;
  public bool isWaiting;
  FlyingMaceBehavior _flyingMaceBehavior;

  void Awake()
  {
    _flyingMaceBehavior = mace.GetComponent<FlyingMaceBehavior>();
    player = GameObject.FindWithTag("Player");
    knockbackTimer = knockbackTimerInit;
  }
  void Update()
  {
    knockbackLogic();
    chaseLogic();
  }
  public void SetKnockbackState()
  {
    beingKnockback = true;
  }
  void knockbackLogic()
  {
    if (beingKnockback == true && knockbackTimer >= .0f)
    {
      float knockbackFallout = knockbackCurve.Evaluate(knockbackTween);
      //_flyingMaceBehavior.hits.transform.position -= _flyingMaceBehavior.hits.normal * knockbackForce * knockbackFallout * Time.deltaTime;
      knockbackTween += Time.deltaTime;
      knockbackTimer -= Time.deltaTime;
      Debug.Log("Ai!");
    }
    else
    {
      knockbackTimer = knockbackTimerInit;
      knockbackTween = .0f;
      beingKnockback = false;
    }
  }
  void chaseLogic()
  {
    transform.LookAt(player.transform);
    this.transform.position += this.transform.forward * walkSpeed * Time.deltaTime;
  }
  void waitLogic()
  {

  }
}
