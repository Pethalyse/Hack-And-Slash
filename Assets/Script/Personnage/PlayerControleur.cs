using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerControleur : PersonnagesControl
{
    bool isInvincible;
    float invincibilityCD = 2.5f;
    public List<GameObject> MoonPaladinWeapons;
    // Update is called once per frame
    void Update()
    {
        if (inLife)
        {

            if(isInvincible)
            {
                invincibilityCD -= Time.deltaTime;

                if(invincibilityCD <= 0)
                {
                    isInvincible = false;
                    invincibilityCD = 2.5f;
                }
            }

            if (!isAttacking && !inAnimation)
            {
                float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
                {
                    return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
                }

                if (Input.GetMouseButtonDown(0) && !isInvincible)
                {
                    /*//Get the Screen positions of the object
                    Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

                    //Get the Screen position of the mouse
                    Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

                    //Get the angle between the points
                    float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

                    //Ta Daaa
                    transform.rotation = Quaternion.Euler(new Vector3(0f, -angle - 90, 0f));
                    */
                    attack();
                }

                if (!attackAnimation)
                {
                    Vector3 move = transform.position;


                    if (Input.GetKey("z"))
                    {
                        Vector3 pos = transform.position;
                        pos.z += MouvementSpeed;
                        transform.position = pos;
                        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    }
                    if (Input.GetKey("q"))
                    {
                        Vector3 pos = transform.position;
                        pos.x -= MouvementSpeed;
                        transform.position = pos;
                        transform.rotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));
                    }
                    if (Input.GetKey("s"))
                    {
                        Vector3 pos = transform.position;
                        pos.z -= MouvementSpeed;
                        transform.position = pos;
                        transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                    }
                    if (Input.GetKey("d"))
                    {
                        Vector3 pos = transform.position;
                        pos.x += MouvementSpeed;
                        transform.position = pos;
                        transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                    }

                    if (move != transform.position)
                    {
                        animator.SetBool("Run", true);
                    }
                    else
                    {
                        animator.SetBool("Run", false);
                    }

                    if (Input.GetKey("z") && Input.GetKey("d")) { transform.rotation = Quaternion.Euler(new Vector3(0f, 45f, 0f)); }
                    if (Input.GetKey("d") && Input.GetKey("s")) { transform.rotation = Quaternion.Euler(new Vector3(0f, 135f, 0f)); }
                    if (Input.GetKey("s") && Input.GetKey("q")) { transform.rotation = Quaternion.Euler(new Vector3(0f, 225f, 0f)); }
                    if (Input.GetKey("q") && Input.GetKey("z")) { transform.rotation = Quaternion.Euler(new Vector3(0f, 295f, 0f)); }
                }

            }


        }
    }

    public override void takeDamage(int damage)
    {
        if (!isInvincible)
        {
            base.takeDamage(damage);
        }
    }

    public void onInvincible() { isInvincible= true; inAnimation = true; }
    public void notInvincible() { isInvincible= false; }
}
