using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] float attackRadius = 5f;
    [SerializeField] int attackDamage;
    [SerializeField] LayerMask attackLayer;

    protected virtual void Update()
    {
        Attack();
    }

    protected virtual void Attack()
    {
        // 3D 공간에서 특정 위치를 중심으로 한 원형 반경 내에서 모든 콜라이더를 감지하는 함수
        // 범위 안에 감지된 모든 콜라이더 객체들을 배열로 반환한다.
        // 콜라이더[] 변수명 = Physics.OverlapSphere(감지를 시작할 위치, 감지 범위, 감지할 레이어 마스크);
        Collider[] hit = Physics.OverlapSphere(transform.position, attackRadius, attackLayer);

        // foreach를 이용해 hit 배열에 담긴 콜라이더를 enemy 로컬 변수에 저장하여 순회하며 반복한다.
        foreach (Collider enemy in hit)
        {
            enemy.GetComponent<ShieldAndHealthSystem>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
