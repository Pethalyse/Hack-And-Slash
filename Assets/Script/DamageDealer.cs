using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    bool canDamage = false;
    List<GameObject> alreadyHit;
    [SerializeField] int layerMask;

    [SerializeField] float weaponRange;
    [SerializeField] int weaponDamage;

    // Start is called before the first frame update
    void Start()
    {
        alreadyHit= new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDamage)
        {
            RaycastHit[] hits;

            hits = Physics.RaycastAll(transform.position, -transform.up*weaponRange);

            foreach(RaycastHit hit in hits)
            {
                if (!alreadyHit.Contains(hit.transform.gameObject) && hit.transform.gameObject.layer == layerMask)
                {
                    print("Damage");
                    if (layerMask == 6)
                    {
                        hit.transform.gameObject.GetComponent<EnemyControleur>().takeDamage(weaponDamage);
                    }
                    else if(layerMask == 7)
                    {
                        hit.transform.gameObject.GetComponent<PlayerControleur>().takeDamage(weaponDamage);
                    }
                    alreadyHit.Add(hit.transform.gameObject);
                }
            }


        }
    }

    public void activeDamage() { canDamage = true; alreadyHit.Clear(); }
    public void desactiveDamage() { canDamage = false; }

    //saw attack range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponRange);
    }
}
