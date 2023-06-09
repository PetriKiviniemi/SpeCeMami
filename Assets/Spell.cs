using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;


public enum SpellType
{
    DASH
}

static class SpellTypeExtensions
{
    public static SpellScriptableObject From(this SpellType spellType)
    {
        switch(spellType)
        {
            case SpellType.DASH: return ScriptableObject.CreateInstance<Dash>();
            default: throw new ArgumentOutOfRangeException(spellType.ToString());
        }
    }
}

public class Spell : MonoBehaviour{

    public void castSpell(GameObject caster, SpellType spellToCast)
    {
        var spell_obj = SpellTypeExtensions.From(spellToCast);
        StartCoroutine(spell_obj.activateSpellLifecycle(gameObject, caster));
    }
}

