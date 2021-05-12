using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RayCastResult { NoHit, TagHit, HitNoTag }

public static class RayCastHelper
{
    private static Ray ray;
    public static RaycastHit LastHitInfos;
    public static RayCastResult LastRayCastResult;

    /// <summary>
    /// Shoots a Raycast from the given point, to the given direction, for the given distance, and checks the given tags if the ray hits something.
    /// Returns HitNoTag if something has been hit, NoHit if nothing has been hit, TagHit if one of the given tags has been hit.
    /// </summary>
    /// <param name="startingPoint"></param>
    /// <param name="direction"></param>
    /// <param name="distance"></param>
    /// <param name="tagsToCompare"></param>
    /// <returns></returns>
    public static RayCastResult RayCast(ref Vector3 startingPoint, ref Vector3 direction, ref float distance, ref string[] tagsToCompare)
    {
        ray = new Ray(startingPoint, direction);

        if (Physics.Raycast(ray, out LastHitInfos, distance))
        {
            for (int i = 0; i < tagsToCompare.Length; i++)
            {
                if (LastHitInfos.collider.gameObject.CompareTag(tagsToCompare[i]))
                {
                    LastRayCastResult = RayCastResult.TagHit;
                    return LastRayCastResult;
                }
            }

            LastRayCastResult = RayCastResult.HitNoTag;
            return LastRayCastResult;
        }

        LastRayCastResult = RayCastResult.NoHit;
        return LastRayCastResult;
    }

    /// <summary>
    /// Shoots a Raycast from the given point, to the given direction, for the given distance.
    /// Returns HitNoTag if something has been hit, NoHit if nothing has been hit.
    /// </summary>
    /// <param name="startingPoint"></param>
    /// <param name="direction"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static RayCastResult RayCast(ref Vector3 startingPoint, ref Vector3 direction, ref float distance)
    {
        ray = new Ray(startingPoint, direction);

        if (Physics.Raycast(ray, out LastHitInfos, distance))
        {
            LastRayCastResult = RayCastResult.HitNoTag;
            return LastRayCastResult;
        }

        LastRayCastResult = RayCastResult.NoHit;
        return LastRayCastResult;
    }

    /// <summary>
    /// Shoots a Raycast from the given point, to the given direction, for the given distance, and checks the given tag if the ray hits something.
    /// Returns HitNoTag if something has been hit, NoHit if nothing has been hit, TagHit if the given tag has been hit.
    /// </summary>
    /// <param name="startingPoint"></param>
    /// <param name="direction"></param>
    /// <param name="distance"></param>
    /// <param name="tagToCompare"></param>
    /// <returns></returns>
    public static RayCastResult RayCast(ref Vector3 startingPoint, ref Vector3 direction, ref float distance, ref string tagToCompare)
    {
        ray = new Ray(startingPoint, direction);

        if (Physics.Raycast(ray, out LastHitInfos, distance))
        {
            if (LastHitInfos.collider.gameObject.CompareTag(tagToCompare))
            {
                LastRayCastResult = RayCastResult.TagHit;
                return LastRayCastResult;
            }

            LastRayCastResult = RayCastResult.HitNoTag;
            return LastRayCastResult;
        }

        LastRayCastResult = RayCastResult.NoHit;
        return LastRayCastResult;
    }
}
