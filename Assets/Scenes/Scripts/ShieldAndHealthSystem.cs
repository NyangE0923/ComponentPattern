using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ShieldAndHealthSystem : HealthSystem
{
    [SerializeField] protected int shield;
    [SerializeField] protected int maxShield;
    [SerializeField] protected int restorationShieldPoint;
    [SerializeField] protected float restorationShieldTimer = 2f;
    [SerializeField] protected bool restorationShieldEnabled = false;
    protected override void Start()
    {
        base.Start();
        shield = Mathf.Clamp(shield, 0, maxShield);
        shield = maxShield;
    }

    public override void TakeDamage(int damage)
    {
        if (shield > 0)
        {
            if (shield > damage)
            {
                shield -= damage;
                Debug.Log($"적에게 damage : {damage}만큼 공격! (쉴드에 의해 차단)");
            }
            else
            {
                int remainingDamage = damage - shield;
                shield = 0;

                health -= remainingDamage;
                Debug.Log($"적에게 remainingDamage : {remainingDamage}만큼 공격! (쉴드 소모)");

                if(health <= 0)
                {
                    Die();
                    health = 0;
                }
            }
        }
        else
        {
            int totalDamage = damage - defensive;
            if (totalDamage < 0) totalDamage = 0;

            health -= totalDamage;
            Debug.Log($"적에게 totalDamage : {totalDamage}만큼 공격! (방어력이 계산 됨)");

            if(health <= 0)
            {
                Die();
                health = 0;
            }
        }
    }

    protected override void Update()
    {
        if(!restorationShieldEnabled && shield <= maxShield - 1) // 쉴드 회복 코루틴이 false 이며 shield가 (maxShield - 1) 미만 이라면
        {
            StartCoroutine(RestorationShield()); // 쉴드 회복 코루틴을 실행한다.
        }
    }

    IEnumerator RestorationShield() // 쉴드 회복 코루틴
    {
        restorationShieldEnabled = true;
        shield += restorationShieldPoint;
        yield return new WaitForSeconds(restorationShieldTimer);
        restorationShieldEnabled = false;
    }
}
