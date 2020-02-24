using System;
using System.Collections;
using System.Collections.Generic;
using GameEnumSpace;
using UnityEngine;

public class AmmunitionInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public int Batteries = 100;
    public int Bullets = 100;
    public int Arrows = 100;
    public int Bubbles = 100;

    [Space(10)] public int maxBatteries = 400;
    public int maxBullets = 600;
    public int maxArrows = 50;
    public int maxBubbles = 2000;

    public bool addAmmunition(AmmoTypes ammoType, int Volume)
    {
        switch (ammoType)
        {
            case AmmoTypes.Battery:
                if (Batteries >= maxBatteries)
                    return false;
                Batteries += Volume;
                if (Batteries >= maxBatteries)
                    Batteries = maxBatteries;
                break;
            case AmmoTypes.Bubbles:
                if (Bubbles >= maxBubbles)
                    return false;
                Bubbles += Volume;
                if (Bubbles >= maxBubbles)
                    Bubbles = maxBubbles;
                break;
            case AmmoTypes.Bullets:
                if (Bullets >= maxBullets)
                    return false;
                Bullets += Volume;
                if (Bullets >= maxBullets)
                    Bullets = maxBullets;
                break;
            case AmmoTypes.Arrows:
                if (Arrows >= maxArrows)
                    return false;
                Arrows += Volume;
                if (Arrows >= maxArrows)
                    Arrows = maxArrows;
                break;
        }

        return true;
    }

    public bool subAmmunition(AmmoTypes ammoType)
    {
        switch (ammoType)
        {
            case AmmoTypes.Battery:
                if (Batteries == 0)
                    return false;
                Batteries--;
                break;
            case AmmoTypes.Bubbles:
                if (Bubbles == 0)
                    return false;
                Bubbles--;
                break;
            case AmmoTypes.Bullets:
                if (Bullets == 0)
                    return false;
                Bullets--;
                break;
            case AmmoTypes.Arrows:
                if (Arrows == 0)
                    return false;
                Arrows--;
                break;
        }

        return true;
    }

    //Checks if Magazine is Empty, False when Empty
    public bool magEmpty(AmmoTypes ammoType)
    {
        switch (ammoType)
        {
            case AmmoTypes.Battery:
                if (Batteries == 0)
                    return false;
                break;
            case AmmoTypes.Bubbles:
                if (Bubbles == 0)
                    return false;
                break;
            case AmmoTypes.Bullets:
                if (Bullets == 0)
                    return false;
                break;
            case AmmoTypes.Arrows:
                if (Arrows == 0)
                    return false;
                break;
        }

        return true;
    }

    public int currentAmmoSize(AmmoTypes ammoType)
    {
        switch (ammoType)
        {
            case AmmoTypes.Battery:
                return Batteries;
                break;
            case AmmoTypes.Bubbles:
                return Bubbles;
                break;
            case AmmoTypes.Bullets:
                return Bullets;
                break;
            case AmmoTypes.Arrows:
                return Arrows;
                break;
        }

        return -1;
    }
}