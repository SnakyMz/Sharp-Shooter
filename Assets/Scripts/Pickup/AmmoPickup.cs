using UnityEngine;

public class AmmoPickup : Pickup
{
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        Debug.Log("Picked up Ammo");
    }
}
