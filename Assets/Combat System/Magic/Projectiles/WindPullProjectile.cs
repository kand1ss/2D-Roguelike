using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPullProjectile : ProjectileBase
{
    [SerializeField] private float pullForce = 4f;
    [SerializeField] private float pullDuration = 2f;

    private Vector3 targetPosition;
    private float distanceToTarget;

    private List<Rigidbody2D> entitiesInTornado = new List<Rigidbody2D>();

    private Coroutine destroyCoroutine;


    protected override void Start()
    {
        base.Start();

        ProjectileCollision.enabled = false;

        TurnSpellParticles(false);

        targetPosition = InputService.Instance.GetCursorPositionInWorldPoint();
        distanceToTarget = Vector3.Distance(StartPosition, targetPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Entity entity))
        {
            Rigidbody2D entityRb = entity.GetComponent<Rigidbody2D>();

            if (!entitiesInTornado.Contains(entityRb))
                entitiesInTornado.Add(entityRb);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Entity entity))
        {
            Rigidbody2D entityRb = entity.GetComponent<Rigidbody2D>();

            if (entitiesInTornado.Contains(entityRb))
                entitiesInTornado.Remove(entityRb);
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, StartPosition);
        if (distance > distanceToTarget)
        {
            ProjectileRb.velocity = Vector3.zero;
            ProjectileCollision.enabled = true;

            TurnSpellParticles(true);
            PullObjectsToCenter();

            if(destroyCoroutine == null)
                destroyCoroutine = StartCoroutine(WaitForDestroy());
        }
    }

    private void PullObjectsToCenter()
    {
        ProjectileImpact();
        foreach (Rigidbody2D entityRb in entitiesInTornado)
        {
            Vector2 pullDirection = (transform.position - entityRb.transform.position).normalized;
            entityRb.AddForce(pullDirection * pullForce, ForceMode2D.Force);
        }
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(pullDuration);

        entitiesInTornado.Clear();
        destroyCoroutine = null;

        DestroyProjectile();
    }
}