using UnityEngine;
using System.Collections;
using System;

public class MortorFireControl : MonoBehaviour {

    [Tooltip("Manipulate the velocity to work in the zone of the city. The farther the tower is from the player the larger the hit zone.")]
    [SerializeField]
    float initialFireVelocity = 300;
    [SerializeField]
    Transform targetTransform;
    [SerializeField]
    Transform towerTop;
    [SerializeField]
    float rotationSpeed = 2;
    [SerializeField]
    Rigidbody shellRigidbody;
    [SerializeField]
    Transform fireTransform;
    [SerializeField]
    float launchForce = 150f;

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire2"))
            Fire();
	}

    void FixedUpdate() {
        //targetDirection.x = CalculateTrajectoryAngle();

        TurnTowardsTarget();

    }
    
    private void TurnTowardsTarget() {
        float x = towerTop.position.x - targetTransform.position.x;
        float z = towerTop.position.z - targetTransform.position.z;
        float lateratDistance = Mathf.Sqrt((x * x) + (z * z));

        Vector3 targetDirection = targetTransform.position - towerTop.position;

        targetDirection.y = Mathf.Tan(CalculateTrajectoryAngle()) * lateratDistance;

        Debug.DrawLine(towerTop.position, targetDirection);

        float step = rotationSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(towerTop.forward, targetDirection, step, 0.0f);
        
        towerTop.rotation = Quaternion.LookRotation(newDir);
    }

    private float CalculateTrajectoryAngle() {
        var y = targetTransform.position.y - fireTransform.position.y;
        var x = (targetTransform.position - fireTransform.position).magnitude;
        var v = initialFireVelocity;
        var g = Mathf.Abs(Physics.gravity.y);
        var sqrt = (v * v * v * v) - (g * (g * (x * x) + 2 * y * (v * v)));
        if (sqrt > 0)
        {
            sqrt = Mathf.Sqrt(sqrt);
            var calculatedAnglePos = Mathf.Atan(((v * v) + sqrt) / (g * x)) * Mathf.Rad2Deg;
            var calculatedAngleNeg = Mathf.Atan(((v * v) - sqrt) / (g * x)) * Mathf.Rad2Deg;

            if (0 < calculatedAnglePos && calculatedAnglePos < 80)
                return Mathf.Abs(calculatedAnglePos);
            else if (calculatedAngleNeg >= 0 && calculatedAngleNeg <= 80)
                return Mathf.Abs(calculatedAngleNeg);
        }
        
        Debug.Log("Did not find matching trajectory");
        return 0.0f;
            
    }

    private void Fire() {
        Rigidbody newShell = GameObject.Instantiate(shellRigidbody, fireTransform.position, fireTransform.rotation) as Rigidbody;
        newShell.velocity = launchForce * fireTransform.forward;
    }
}
