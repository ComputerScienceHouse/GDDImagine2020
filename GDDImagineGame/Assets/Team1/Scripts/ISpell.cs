using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    // cost of spell
    int cost { get; }
    
    // damage of the spell 
    int damage { get; }
}
