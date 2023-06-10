using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SpellScriptableObject : ScriptableObject 
{
    public SpellType spellType;
    public string spellSOName;
    public Image spellIcon;
    protected ulong _spellGUID;
    protected float _castTime;
    protected float _cooldown;
    protected Animation _spellAnimation;
    protected bool _castSuccess;
    protected GameManager gameManager;

    public IEnumerator activateSpellLifecycle(GameObject spellPrefab, GameObject caster)
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        yield return initialize(caster);
        GameObject.Destroy(spellPrefab);
    }

    public abstract IEnumerator initialize(GameObject caster);

   //For castable spells, implement
   // public float relativeCastTime(int unitHaste)
   // {
   //     return _castTime / (1 + UtilityMethods.statToPercent(unitHaste));
   // }
   //
   // public float getCastTime() { return _castTime; }

   // protected IEnumerator WaitForSpellCast(GameObject caster, float castTime)
   // {
   //     while(!caster.GetComponent<Unit>().getIsMoving() && castTime > 0.0f)
   //     {
   //         yield return null;
   //         castTime -= Time.deltaTime;
   //     }

   //     if (castTime > 0.0f)
   //         _castSuccess = false;
   //     else
   //         _castSuccess = true;
   // }
}
