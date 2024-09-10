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
        // 3D �������� Ư�� ��ġ�� �߽����� �� ���� �ݰ� ������ ��� �ݶ��̴��� �����ϴ� �Լ�
        // ���� �ȿ� ������ ��� �ݶ��̴� ��ü���� �迭�� ��ȯ�Ѵ�.
        // �ݶ��̴�[] ������ = Physics.OverlapSphere(������ ������ ��ġ, ���� ����, ������ ���̾� ����ũ);
        Collider[] hit = Physics.OverlapSphere(transform.position, attackRadius, attackLayer);

        // foreach�� �̿��� hit �迭�� ��� �ݶ��̴��� enemy ���� ������ �����Ͽ� ��ȸ�ϸ� �ݺ��Ѵ�.
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
