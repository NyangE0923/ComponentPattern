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
                Debug.Log($"������ damage : {damage}��ŭ ����! (���忡 ���� ����)");
            }
            else
            {
                int remainingDamage = damage - shield;
                shield = 0;

                health -= remainingDamage;
                Debug.Log($"������ remainingDamage : {remainingDamage}��ŭ ����! (���� �Ҹ�)");

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
            Debug.Log($"������ totalDamage : {totalDamage}��ŭ ����! (������ ��� ��)");

            if(health <= 0)
            {
                Die();
                health = 0;
            }
        }
    }

    protected override void Update()
    {
        if(!restorationShieldEnabled && shield <= maxShield - 1) // ���� ȸ�� �ڷ�ƾ�� false �̸� shield�� (maxShield - 1) �̸� �̶��
        {
            StartCoroutine(RestorationShield()); // ���� ȸ�� �ڷ�ƾ�� �����Ѵ�.
        }
    }

    IEnumerator RestorationShield() // ���� ȸ�� �ڷ�ƾ
    {
        restorationShieldEnabled = true;
        shield += restorationShieldPoint;
        yield return new WaitForSeconds(restorationShieldTimer);
        restorationShieldEnabled = false;
    }
}
