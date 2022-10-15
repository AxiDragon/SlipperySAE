using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierManager : MonoBehaviour
{
    [HideInInspector] public FishMove fish;

    private void Start()
    {
        fish = GetComponent<FishMove>();
    }

    public void ModifyModifier(float value, bool increasing)
    {
        fish.modifier = increasing ? fish.modifier + value : value;
    }

    public void IncreasePower(float value)
    {
        fish.jumpPower += value * 3f;
        fish.power += value * 3f;
    }
}
