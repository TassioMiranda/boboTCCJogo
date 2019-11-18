using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeLogic : MonoBehaviour
{
    [Header("Melee References")]
    [SerializeField] GameObject prefabMace;
    [SerializeField] Camera fpsCam;
    [SerializeField] Transform maceSpawn;
    [SerializeField] GameObject prefabBullet;
    [SerializeField] string hitButton;
    [SerializeField] string throwButton;

    [Header ("Melee Settings")]
    public Animator wepAnimation;
    BoxCollider wepCollider;
    [SerializeField] float swingTimerInit;
    [SerializeField] float swingTimer;
    [SerializeField] float smoothReturn;
    [SerializeField] float flyingMaceSpeed;
    public bool flyBack;

    void Start()
    {
      wepAnimation = GetComponent<Animator>();
      wepCollider = this.transform.gameObject.GetComponent<BoxCollider>();
      wepCollider.enabled = false;
      swingTimer = swingTimerInit;
      flyBack = false;
    }

    void Update()
    {
      swingTimer -= Time.deltaTime;
      if(!wepAnimation.GetBool("isMaceThrown"))
      {
        MaceMelee();
      }
        MaceThrown();
      if(flyBack == true)
      {
        MaceFlyBack();
      }
    }
    void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.CompareTag("Enemy"))
      {
        other.gameObject.GetComponent<EnemyBetterBehavior>().SetKnockbackStateDirection(-1 * (fpsCam.transform.forward));
        other.gameObject.GetComponent<EnemyBetterBehavior>().SetKnockbackState();
        Debug.Log("YEA SCIENCE");
      }
      if(other.gameObject.CompareTag("Bullet"))
      {
        Object.Destroy(other.gameObject);
        Object.Instantiate(prefabBullet, maceSpawn.transform.position,fpsCam.transform.rotation);
      }
    }

    void MaceMelee()
    {
      if(Input.GetButtonDown(hitButton) && swingTimer <= 0)
      {
        Debug.Log(hitButton);
        if(wepAnimation.GetBool("swingRL") == false)
        {
          wepAnimation.SetBool("swingRL", true);
        }
        else if(wepAnimation.GetBool("swingRL") == true)
        {
          wepAnimation.SetBool("swingRL", false);
        }
        swingTimer = swingTimerInit;
      }
    }
    void MaceThrown()
    {
      if(Input.GetButtonDown(throwButton))
      {
        if(!GameObject.FindWithTag("FlyingMace"))
        {
          GetComponent<Renderer>().enabled = false;
          wepAnimation.SetBool("isMaceThrown", true);
          GameObject newMace = Object.Instantiate(prefabMace, maceSpawn.transform.position,fpsCam.transform.rotation);
        }
        else if(GameObject.FindWithTag("FlyingMace"))
        {
          flyBack = true;
        }
      }
    }
    void MaceFlyBack()
    {
      GameObject.FindWithTag("FlyingMace").transform.position = Vector3.Lerp(GameObject.FindWithTag("FlyingMace").transform.position, fpsCam.gameObject.transform.position, flyingMaceSpeed * smoothReturn * Time.deltaTime);
      GameObject.FindWithTag("FlyingMace").transform.LookAt(fpsCam.gameObject.transform);
    }
}
