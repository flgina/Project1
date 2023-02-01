using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : MonoBehaviour
{
    // set enemy health
    public int CurrentDamage;
    public int damage;
    public int targetDamage;

    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
        CurrentDamage = 0;
        targetDamage = 2;
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

        Debug.Log("damage = " + damage);
        Debug.Log("CurrentDamage = " + CurrentDamage);
        Debug.Log("targetDamage = " + targetDamage);

        if (targetDamage == CurrentDamage)
        {
            Destroy(gameObject);
            Debug.Log("damage = " + damage);
            Debug.Log("CurrentDamage = " + CurrentDamage);
            Debug.Log("targetDamage = " + targetDamage);
        }
    }
}
