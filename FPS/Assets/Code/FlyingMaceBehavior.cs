using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMaceBehavior : MonoBehaviour
{
  public float flyingSpeed = 5f;
  MeleeLogic meleeLogicScript;
  Vector3 mPreviousPos;
  public RaycastHit[] hits;
  void Start()
  {
    mPreviousPos = this.transform.position;
    meleeLogicScript = GameObject.FindWithTag("Weapon").GetComponent<MeleeLogic>();
  }
  void Update()
  {
    mPreviousPos = this.transform.position;

    this.transform.position += this.transform.forward * flyingSpeed * Time.deltaTime;

    hits = Physics.RaycastAll(new Ray(mPreviousPos,(this.transform.position - mPreviousPos).normalized),(this.transform.position - mPreviousPos).magnitude);

    for(int i = 0; i < hits.Length; i++)
    {
      if(hits[i].collider.gameObject.name == "Player")
      {
        Debug.Log(hits[i].collider.gameObject.name);
        meleeLogicScript.flyBack = false;
        meleeLogicScript.wepAnimation.SetBool("isMaceThrown",false);
        meleeLogicScript.GetComponent<Renderer>().enabled = true;
        Object.Destroy(this.transform.gameObject);

      }
      if(hits[i].collider.gameObject.name == "Enemy")
      {
        GameObject Enemy = hits[i].collider.gameObject;
        hits[i].collider.gameObject.GetComponent<EnemyBetterBehavior>().SetKnockbackStateDirection(hits[i].normal);
        hits[i].collider.gameObject.GetComponent<EnemyBetterBehavior>().SetKnockbackState();
      }
    }
    Debug.DrawLine(this.transform.position, mPreviousPos);
  }
}
