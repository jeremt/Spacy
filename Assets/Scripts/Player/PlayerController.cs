using System;
using UnityEngine;

public struct RaycastOriginInfos {

    public Vector2 TopLeft, TopRight;
    public Vector2 BottomLeft, BottomRight;

    public override string ToString() {
        return String.Format(
            "TopLeft: {0}, TopRight: {1}, BottomLeft: {2}, BottomRight: {3}",
            TopLeft, TopRight, BottomLeft, BottomRight);
    }
}
public struct CollisionInfos {

    public bool Above, Below;
    public bool Left, Right;
    public bool BelowPlatform;

    public void Reset() { Above = Below = Left = Right = BelowPlatform = false; }

    public override string ToString() {
        return String.Format(
            "Above: {0}, Below: {1}, Left: {2}, Right: {3}, BelowPlatform: {4}",
            Above, Below, Left,Right, BelowPlatform);
    }
}

[RequireComponent(typeof (BoxCollider2D))]
public class PlayerController : MonoBehaviour {

    private const float SkinWidth = .015f;

    // Controller API
    public LayerMask ObstacleLayerMask;
    public LayerMask PlatformLayerMask;
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

        RaycastOrigins.TopLeft.Set(bounds.min.x, bounds.max.y);
        RaycastOrigins.TopRight.Set(bounds.max.x, bounds.max.y);
        RaycastOrigins.BottomLeft.Set(bounds.min.x, bounds.min.y);
        RaycastOrigins.BottomRight.Set(bounds.max.x, bounds.min.y);
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
            var rayOrigin = ((directionX == -1) ? RaycastOrigins.BottomLeft : RaycastOrigins.BottomRight) + Vector2.up * (_horizontalRaySpacing * i);
            var hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, ObstacleLayerMask);

            if (hit) {
                velocity.x = (hit.distance - SkinWidth) * directionX;
                rayLength = hit.distance;
                Collisions.Left = directionX == -1;
                Collisions.Right = directionX == 1;
            }
            // Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.green);
        }
    }

    private void VerticalCollisions(ref Vector3 velocity) {
        var directionY = Math.Sign(velocity.y);
        var rayLength = Mathf.Abs(velocity.y) + SkinWidth;
        for (var i = 0; i < VerticalRayCount; i++) {
            var rayOrigin = ((directionY == -1) ? RaycastOrigins.BottomLeft : RaycastOrigins.TopLeft) + Vector2.right * (_verticalRaySpacing * i + velocity.x);
            var hitObstacle = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, ObstacleLayerMask);

            if (hitObstacle) {
                velocity.y = (hitObstacle.distance - SkinWidth) * directionY;
                rayLength = hitObstacle.distance;
                Collisions.Below = directionY == -1;
                Collisions.Above = directionY == 1;
            }

            if (directionY == -1) {
                var hitPlatform = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, PlatformLayerMask);

                if (hitPlatform && RaycastOrigins.BottomLeft.y >= hitPlatform.collider.bounds.max.y) {
                    velocity.y = (hitPlatform.distance - SkinWidth) * directionY;
                    rayLength = hitObstacle.distance;
                    Collisions.BelowPlatform = Collisions.Below = directionY == -1;
                    Collisions.Above = directionY == 1;
                }
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