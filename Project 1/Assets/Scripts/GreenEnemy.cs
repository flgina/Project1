using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : MonoBehaviour
{
    // set normal attack
    public int CurrentDamage;
    public int damage;
    public int targetDamage;

    // set fireball attack
    public int CurrentFireballDamage;
    public int FireballDamage;
    public int targetFireballDamage;

    // Start is called before the first frame update
    void Start()
    {
        // normal attack
        damage = 0;
        CurrentDamage = 0;
        targetDamage = 2;
        
        // fireball
        FireballDamage = 0;
        CurrentFireballDamage = 0;
        targetFireballDamage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    // updates damage
    public void UpdateDamage(int damage)
    {
        CurrentDamage += damage;
        if (targetDamage == CurrentDamage)
        {
            Destroy(gameObject);
            Debug.Log("damage = " + damage);
            Debug.Log("CurrentDamage = " + CurrentDamage);
            Debug.Log("targetDamage = " + targetDamage);
        }
    }
    public void UpdateFireballDamage(int FireballDamage)
    {
        CurrentFireballDamage += FireballDamage;
        if (targetFireballDamage == CurrentFireballDamage)
        {
            Destroy(gameObject);
            Debug.Log("damage = " + FireballDamage);
            Debug.Log("CurrentDamage = " + CurrentFireballDamage);
            Debug.Log("targetDamage = " + targetFireballDamage);
        }
    }
}
