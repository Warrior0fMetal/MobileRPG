using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{

    public float maxHP = 100;
    public float currHP;
    public float maxMP = 100;
    public float currMP;
    public Slider hpBar;
    public float damage = 29;
    void Start()
    {
        currHP = maxHP;

    }
    void Update()
    {
        SetHealth();
        
    }

    void SetHealth()
    {
        hpBar.value = currHP;
    }

    // Start is called before the first frame update
  
    public void TakeDMG()
    {
        currHP -= damage;
    }

}
