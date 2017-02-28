using UnityEngine;
using System.Collections;

public class MouseTargetControl : MonoBehaviour {
    [SerializeField]
    Camera cameraMain;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    Transform player1Transform;
    [SerializeField]
    float maxDistanceBetweenPlayers = 50f;
    [SerializeField]
    float maxCursorUpPercent = .66f;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float followSpeedIncreaseRange = 20f;

    // Update is called once per frame
    void FixedUpdate () {
        Move();
    }

    private void Move() {
        RaycastHit rayCasthit;
        float maxDistanceToRaycast = 500f;
        bool isCollidingWithGround = Physics.Raycast(cameraMain.ScreenPointToRay(Input.mousePosition), out rayCasthit, maxDistanceToRaycast, groundLayer);
        bool isPlayerWithinMaxDistanceBetweenCharacters = Mathf.Abs(Vector3.Distance(player1Transform.position, transform.position)) <= maxDistanceBetweenPlayers;
        bool isCursorCloserToOtherPlayer = Vector3.Distance(player1Transform.position, transform.position) > Vector3.Distance(rayCasthit.point, player1Transform.position);
        bool mouseIsInAllowableArea = Input.mousePosition.y <= (maxCursorUpPercent * Screen.height);
        bool PlayerCanMove = isCollidingWithGround && (isPlayerWithinMaxDistanceBetweenCharacters || isCursorCloserToOtherPlayer) && mouseIsInAllowableArea;
        if (PlayerCanMove)
        {
            bool isInFollowRange = (Vector3.Distance(transform.position, rayCasthit.point) <= followSpeedIncreaseRange);
            float tempMovementSpeed = movementSpeed;

            if (!isInFollowRange)
                tempMovementSpeed *= 5f;

            float stepTowardsPointer = tempMovementSpeed * Time.deltaTime;

            Vector3 newPosition = Vector3.MoveTowards(transform.position, rayCasthit.point, stepTowardsPointer);
            transform.position = new Vector3(newPosition.x, 0.0f, newPosition.z);
            
        }
    }
}
