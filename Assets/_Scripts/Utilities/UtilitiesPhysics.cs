using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static partial class Utilities
{
    public static bool BoundsIntersectXAxis(Bounds bound1, Bounds bound2)
    {
        Bounds bound1New = new Bounds(new Vector3(bound1.center.x, 0.0f, 0.0f), new Vector3(bound1.size.x, 1f, 0.1f));

        Bounds bound2New = new Bounds(new Vector3(bound2.center.x, 0.0f, 0.0f), new Vector3(bound2.size.x, 1f, 0.1f));

        if (bound1New.Intersects(bound2New))
            return true;
        return false;
    }

    public static bool BoundsIntersect2D(Bounds bound1, Bounds bound2)
    {
        Bounds newBound1 = new Bounds(new Vector3(bound1.center.x, bound1.center.y, 0.0f), new Vector3(bound1.size.x, bound1.size.y, 0.1f));

        Bounds newBound2 = new Bounds(new Vector3(bound2.center.x, bound2.center.y, 0.0f), new Vector3(bound2.size.x, bound2.size.y, 0.1f));

        if (newBound1.Intersects(newBound2))
            return true;

        return false;
    }

    public static bool BoundContains2D(Bounds bound, Vector3 point)
    {
        Bounds tempBound = new Bounds(new Vector3(bound.center.x, bound.center.y, 0.0f), new Vector3(bound.size.x, bound.size.y, 0.1f));

        Vector3 tempPoint = new Vector3(point.x, point.y, 0.0f);

        if (tempBound.Contains(tempPoint))
            return true;
        return false;
    }

    public static bool InAreaX(float areaCenterX, float areaRadius, Bounds targetBounds)
    {
        Bounds areaBounds = new Bounds(new Vector3(areaCenterX, 0.0f, 0.0f), new Vector3(areaRadius * 2, 1.0f, 0.1f));

        Bounds newTargetBounds = new Bounds(new Vector3(targetBounds.center.x, 0.0f, 0.0f), new Vector3(0.1f, 1f, 0.1f));

        if (areaBounds.Intersects(newTargetBounds))
            return true;
        else
            return false;
    }

    public static bool IsObjectTouched(Collider2D collider2D, Camera camera)
    {
        Vector3 inputPos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 inputPos2D = new Vector2(inputPos.x, inputPos.y);

        if (collider2D == Physics2D.OverlapPoint(inputPos2D))
            return true;
        
        return false;
    }

    public static bool IsObjectTouched(Collider collider, Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 200f))
            return hitInfo.collider == collider;

        return false;
    }

    public static bool IfClickInsideCollider(Collider2D collider, Camera camera)
    {
        Vector3 inputPos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 inputPos2D = new Vector2(inputPos.x, inputPos.y);
        if (collider.OverlapPoint(inputPos2D))
            return true;

        return false;
    }

    public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector3 direction, float distance, LayerMask layerMask, bool drawRay = true)
    {

        RaycastHit2D[] hitArr = Physics2D.RaycastAll(origin, direction, distance, layerMask);

        if (drawRay)
        {
            foreach (RaycastHit2D hit in hitArr)
                if (hit.collider == null)
                    Debug.DrawRay(origin, direction * distance, Color.black, 100);
                else
                    Debug.DrawRay(origin, direction * distance, Color.green, 100);
        }

        return hitArr;
    }

    public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, float distance, LayerMask layerMask, bool drawRay = true)
    {
        RaycastHit2D[] hitArr = Physics2D.GetRayIntersectionAll(ray, distance, layerMask);

        if (drawRay)
        {
            if (hitArr.Length == 0)
                Debug.DrawRay(ray.origin, ray.direction * 200, Color.black, 100);
            
            foreach (RaycastHit2D hit in hitArr)
                Debug.DrawRay(ray.origin, ray.direction * 200, Color.green, 100);
        }

        return hitArr;
    }
}
