using System;
using UnityEngine;

public struct RaycastOriginInfos
{
    public Vector2 topLeft, topRight;
    public Vector2 bottomLeft, bottomRight;
}
public struct CollisionInfos
{
    public bool above, below;
    public bool left, right;

    public void Reset() { above = below = left = right = false; }
}

[RequireComponent(typeof (BoxCollider2D))]
public class PlayerController : MonoBehaviour {
    private const float SkinWidth = .015f;

    // Controller API
    public LayerMask CollisionLayerMask;
    public int HorizontalRayCount = 4;
    public int VerticalRayCount = 4;

    // PlayerController collision and position informations
    public CollisionInfos Collisions;
    public RaycastOriginInfos RaycastOrigins;

    // Components
    private Collider2D _collider;

    // Raycasting
    private float _horizontalRaySpacing;
    private float _verticalRaySpacing;

    public void Awake() { _collider = GetComponent<Collider2D>(); }

    public void Start() { CalculateRaySpacing(); }

    private void UpdateRaycastOrigins() {
        var bounds = _collider.bounds;
        bounds.Expand(SkinWidth * -2);

        RaycastOrigins.topLeft.Set(bounds.min.x, bounds.max.y);
        RaycastOrigins.topRight.Set(bounds.max.x, bounds.max.y);
        RaycastOrigins.bottomLeft.Set(bounds.min.x, bounds.min.y);
        RaycastOrigins.bottomRight.Set(bounds.max.x, bounds.min.y);
    }

    private void CalculateRaySpacing() {
        var bounds = _collider.bounds;
        bounds.Expand(SkinWidth * -2);

        HorizontalRayCount = Mathf.Clamp(HorizontalRayCount, 2, int.MaxValue);
        VerticalRayCount = Mathf.Clamp(VerticalRayCount, 2, int.MaxValue);
        _horizontalRaySpacing = bounds.size.y / (HorizontalRayCount - 1);
        _verticalRaySpacing = bounds.size.x / (VerticalRayCount - 1);
    }

    private void HorizontalCollisions(ref Vector3 velocity) {
        var directionX = Math.Sign(velocity.x);
        var rayLength = Mathf.Abs(velocity.x) + SkinWidth;
        for (var i = 0; i < HorizontalRayCount; i++) {
            var rayOrigin = ((directionX == -1) ? RaycastOrigins.bottomLeft : RaycastOrigins.bottomRight) + Vector2.up * (_horizontalRaySpacing * i);
            var hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, CollisionLayerMask);

            if (hit) {
                velocity.x = (hit.distance - SkinWidth) * directionX;
                rayLength = hit.distance;
                Collisions.left = directionX == -1;
                Collisions.right = directionX == 1;
            }
            // Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);
        }
    }

    private void VerticalCollisions(ref Vector3 velocity) {
        var directionY = Math.Sign(velocity.y);
        var rayLength = Mathf.Abs(velocity.y) + SkinWidth;
        for (var i = 0; i < VerticalRayCount; i++) {
            var rayOrigin = ((directionY == -1) ? RaycastOrigins.bottomLeft : RaycastOrigins.topLeft) + Vector2.right * (_verticalRaySpacing * i + velocity.x);
            var hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, CollisionLayerMask);

            if (hit) {
                velocity.y = (hit.distance - SkinWidth) * directionY;
                rayLength = hit.distance;
                Collisions.below = directionY == -1;
                Collisions.above = directionY == 1;
            }
            // Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
        }
    }

    public void Move(Vector3 velocity) {
        UpdateRaycastOrigins();
        Collisions.Reset();

        if (Math.Abs(velocity.x) > float.Epsilon) {
            HorizontalCollisions(ref velocity);
        }
        if (Math.Abs(velocity.y) > float.Epsilon) {
            VerticalCollisions(ref velocity);
        }
        transform.Translate(velocity);
    }
}