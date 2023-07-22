using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PersonnagesControl : MonoBehaviour
{

    //Speed
    private float mouvementSpeed = 0.02f;
    public float MouvementSpeed { get => mouvementSpeed; }

    [Header("Stats")]
    //Stats
    [SerializeField]
    private float pvMax = 100;
    public float pvNow;
    public bool inLife = true;

    [Header("Animation")]
    //Animator
    public Animator animator;

    //Block Animation
    [HideInInspector]
    public bool isAttacking = false;
    [HideInInspector]
    public bool attackAnimation = false;
    [HideInInspector]
    public bool inAnimation = false;

    public GameObject currentWeapon;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        pvNow = pvMax;
    }

    //Attaque
    public void attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

    }

    //Attaque 2
    public void attack2()
    {
        isAttacking = true;
        animator.SetTrigger("Attack 2");

    }

    //Spell
    public void spell()
    {
        isAttacking = true;
        animator.SetTrigger("Spell");

    }

    public void lookInLife()
    {
        if (pvNow <= 0)
        {
            inLife = false;
            animator.SetTrigger("Die");
            Rigidbody r;
            r = GetComponent<Rigidbody>();
            r.isKinematic = true;
            r.detectCollisions = false;
        }
    }

    public void stopDealDamage() { isAttacking= false; currentWeapon.GetComponentInChildren<DamageDealer>().desactiveDamage(); }
    public void startDealDamage() { isAttacking= true; attackAnimation = true; currentWeapon.GetComponentInChildren<DamageDealer>().activeDamage(); }
    public void notAttackAnimation() { attackAnimation = false; isAttacking = false; }

    public virtual void takeDamage(int damage) 
    {
        if (pvNow > 0)
        {
            animator.SetTrigger("GetHit"); pvNow -= damage;
        }
        lookInLife(); 
    }

    public void onInAnimation() { inAnimation = true; }
    public void notInAnimation() { inAnimation = false; }
}
