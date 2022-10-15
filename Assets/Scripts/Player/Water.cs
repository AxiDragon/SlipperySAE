using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ModifierManager>(out var mod))
        {
            mod.ModifyModifier(1f, false);
            mod.fish.inWater = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ModifierManager>(out var mod))
        {
            mod.ModifyModifier(.3f * Time.deltaTime, true);
            mod.IncreasePower(3f * Time.deltaTime);
            mod.fish.inWater = true;
        }
    }
}
