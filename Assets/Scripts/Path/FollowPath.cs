using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using FollowType = Assets.Scripts.Utils.Enums.FollowType;
using System;

public class FollowPath : TriggerAble
{
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private float collisionDistance = .2f;

    [SerializeField] private FollowType Type = FollowType.MoveTowards;
    [SerializeField] private PathDefinition Path;
    [SerializeField] private float Speed = 1;
    [SerializeField] private float MaxDistanceToGoal = 0.1f;
    [SerializeField] private bool started = false;

    private IEnumerator<Transform> currentPoint;
    private new Collider2D collider;
    public new void Start()
    {
        base.Start();
        if (Path == null)
        {
            Debug.LogError("Path cannot be null", gameObject);
            return;
        }

        currentPoint = Path.GetPathEnumerator();
        currentPoint.MoveNext();

        if (currentPoint.Current == null)
            return;

        collider = GetComponentInChildren<Collider2D>();

        transform.position = currentPoint.Current.position;
    }

    public void Update()
    {
        if (started)
        {
            if (currentPoint == null || currentPoint.Current == null)
                return;
            Vector3 oldPosition = transform.position;
            if (Type == FollowType.MoveTowards)
            { 
                transform.position = Vector3.MoveTowards(transform.position, currentPoint.Current.position, Time.deltaTime * Speed);
                handleObjectsOnTop(oldPosition, transform.position);
            }
            else if (Type == FollowType.Lerp)
            {
                transform.position = Vector3.Lerp(transform.position, currentPoint.Current.position, Time.deltaTime * Speed);
                handleObjectsOnTop(oldPosition, transform.position);
            }

            var distanceSquared = (transform.position - currentPoint.Current.position).magnitude;
            if (distanceSquared < MaxDistanceToGoal)
                currentPoint.MoveNext();
        }
    }

    private void handleObjectsOnTop(Vector3 oldPosition, Vector3 position)
    {
        HashSet<GameObject> hitObjects = new HashSet<GameObject>();
        Vector3 centerPosition = collider.bounds.center;
        centerPosition.y += collider.bounds.extents.y;
        hitObjects.UnionWith(getHitObjects(centerPosition));

        Vector3 leftPosition = centerPosition;
        leftPosition.x = collider.bounds.min.x + 0.1f;
        hitObjects.UnionWith(getHitObjects(leftPosition));

        Vector3 rightPosition = centerPosition;
        rightPosition.x = collider.bounds.max.x - 0.1f;
        hitObjects.UnionWith(getHitObjects(rightPosition));

        Vector3 movement = position - oldPosition;
        foreach (GameObject obj in hitObjects)
        {
            if (obj.name.Contains("Frostie"))
            {
                obj.transform.parent.parent.Translate(movement, Space.World);
            }else
            {
                obj.transform.Translate(movement, Space.World);
            }
        }
    }

    private IEnumerable<GameObject> getHitObjects(Vector3 position)
    {
        HashSet<GameObject> hitObjects = new HashSet<GameObject>();
        RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.up, collisionDistance, collisionLayers);
        for (int i = 0; i < hits.Length; i++)
        {
            hitObjects.Add(hits[i].collider.gameObject);
        }
        return hitObjects;
    }

    protected override void performAction()
    {
        started = !started;
    }
}