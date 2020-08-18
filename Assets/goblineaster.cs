using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblineaster : Health
{
    protected override void Die() {
        SoundSystem.PlayMusic("pisto", 1);
        transform.position -= Vector3.up * 200;

    }
}
