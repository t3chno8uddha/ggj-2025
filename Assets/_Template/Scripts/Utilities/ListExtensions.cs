using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtensions
{
    public static TObject ChooseBest<TObject>(this IList<TObject> collection,
        Func<TObject, bool> necessaryCondition,
        Func<TObject, TObject, bool> comparer)
        where TObject : class
    {
        if (collection == null || collection.Count <= 0) return null;

        var chosenObject = collection[0];

        for (int i = 1; i < collection.Count; i++)
        {
            var objectAtIndex = collection[i];

            var isBetter = comparer(objectAtIndex, chosenObject);
            var chosenHasNecessaryCondition = necessaryCondition(chosenObject);
            var hasNecessaryCondition = necessaryCondition(objectAtIndex);

            if ((isBetter && hasNecessaryCondition) || (!chosenHasNecessaryCondition && hasNecessaryCondition))
            {
                chosenObject = objectAtIndex;
            }
        }

        return necessaryCondition(chosenObject) ? chosenObject : null;
    }

    public static TObject Random<TObject>(this IList<TObject> collection)
    {
        if (collection == null || collection.Count <= 0) return default(TObject);

        return collection[UnityEngine.Random.Range(0, collection.Count)];
    }
    public static TObject RandomWhere<TObject>(this IList<TObject> collection, Func<TObject, bool> condition)
    {
        if (collection == null || collection.Count <= 0) return default(TObject);

        var chosenObjects = collection.Where(condition);

        if (chosenObjects.Count() <= 0) return default(TObject);

        var chosenObjectList = chosenObjects.ToList();

        return chosenObjectList[UnityEngine.Random.Range(0, chosenObjectList.Count)];
    }

    public static TObject NearestTo<TObject>(this IEnumerable<TObject> collection, Vector3 targetPosition) where TObject : Component
    {
        if (collection == null || collection.Count() == 0) return null;

        TObject nearestObject = null;

        float minSqrDistance = Mathf.Infinity;

        foreach (var o in collection)
        {
            if (o == null) continue;

            float sqrDistance = Vector3.SqrMagnitude(o.transform.position - targetPosition);

            if (sqrDistance < minSqrDistance)
            {
                nearestObject = o;
                minSqrDistance = sqrDistance;
            }
        }

        return nearestObject;
    }

    public static TObject NearestTo<TObject>(this IEnumerable<TObject> collection, Transform targetTransform) where TObject : Component
    {
        return collection.NearestTo(targetTransform.position);
    }

    public static bool HasElement<TObject>(this IEnumerable<TObject> collection) where TObject : Component
    {
        return collection.Count() > 0;
    }
}
