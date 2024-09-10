using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAttackSystem : AttackSystem
{
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            base.Update();
        }
    }
}
