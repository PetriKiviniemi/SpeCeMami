using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CastSpellEvent : CustomEvent 
{

    private GameObject _spellCaster;
    private GameObject _spellPrefab;
    private SpellType _spellToCast;
    public CastSpellEvent(GameObject caster, SpellType spellToCast)
    {
        _spellCaster = caster;
        _spellToCast = spellToCast;
        _spellPrefab = Resources.Load("Spells/SpellPrefab", typeof(GameObject)) as GameObject;
    }

    public override void ProcessEvent()
    {
        GameObject spellObj = GameObject.Instantiate(_spellPrefab, _spellCaster.transform) as GameObject;
        spellObj.GetComponent<Spell>().castSpell(_spellCaster, _spellToCast);
        //updateUI(new List<UIElementType> { UIElementType.TargetUnitFrame });
    }

    public override void PauseEvent()
    {
        //TODO:: IMPLEMENT
    }

//Utilize to update UI
//    public override void updateUI(List<UIElementType> uiElements)
//    {
//        foreach(UIElementType elem in uiElements)
//        {
//            GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>().updateUIElement(elem);
//        }
//    }
}

