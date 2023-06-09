using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Dash : SpellScriptableObject 
{
    private float elapsedTime = 0;
    private float dashTime = 0.2f;
    private float dashLength = 0;
    
    public Dash()
    {
        spellType = SpellType.DASH;
        spellSOName = "Dash";
        _cooldown = 2;
    }

    public float RaycastToDir(Vector3 origin, Vector2 dir, float length)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, length);
        if(hit.collider)
        {
            return hit.distance;
        }
        return length;
    }

    public override IEnumerator initialize(GameObject caster)
    {
        dashLength = 2;

        Vector2 endPos = caster.transform.position;

        //TODO:: Extend to every mob
        MovementDir curDir = caster.GetComponent<PlayerController>().curDir;

        Vector3 rayOrigin = caster.transform.position;
        if (curDir == MovementDir.LEFT)
        {
            rayOrigin = rayOrigin - new Vector3(0.55f, 0, 0);
            endPos.x -= RaycastToDir(rayOrigin, Vector2.left, dashLength);
        }
        if (curDir == MovementDir.RIGHT)
        {
            rayOrigin = rayOrigin + new Vector3(0.55f, 0, 0);
            endPos.x += RaycastToDir(rayOrigin, Vector2.right, dashLength);
        }
        caster.GetComponent<PlayerController>().velocity = new Vector2(0,0);

        //Unlocked
        // if (curDir == MovementDir.UP)
        //     endPos.x += dashLength;

        if (curDir == MovementDir.LEFT || curDir == MovementDir.RIGHT)
        {
            while (elapsedTime < dashTime)
            {
                //TODO:: Make the lerp slower (Smaller fractions)
                caster.transform.position = Vector2.Lerp(caster.transform.position, endPos, (elapsedTime / dashTime));
                elapsedTime += Time.deltaTime;
                //TODO: Call own function (PauseEvent);
                yield return new WaitForSeconds(0.3f);
               // yield return new WaitUntil( () => {
               // });
            }
        }

        yield return null;
    }
}
