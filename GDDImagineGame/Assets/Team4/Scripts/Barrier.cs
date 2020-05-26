using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Player.Alliance alliance;

    public void setAlliance(Player.Alliance alliance)
    {
        this.alliance = alliance;
    }
}
