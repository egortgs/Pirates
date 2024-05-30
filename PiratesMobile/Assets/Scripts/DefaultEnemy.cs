using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class DefaultEnemy : Enemy
{
    protected override void Move()
    {
        if (currentDistance < detectionDistance && currentDistance > attackDistance)
        {
            currentSpeed += Time.deltaTime;
            var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f * Time.deltaTime);
            rb.MovePosition(transform.position + transform.forward * currentSpeed * Time.deltaTime);
        }
    }

    protected override void Attack()
    {
        timer += Time.deltaTime;
       
        if (currentDistance < attackDistance)
        {
            currentSpeed -= Time.deltaTime;
            var direction = (player.transform.position - transform.position).normalized;

            if (-transform.right == direction)
            {

                transform.Rotate(0, 0.01f, 0); //rotate a smidge on the y axis to get less than 180 degrees
            }

            Vector3 axis = Vector3.Cross(transform.right, direction);
            float angle = Mathf.Sqrt(Vector3.Dot(direction, direction) * Vector3.Dot(transform.right, transform.right)) + Vector3.Dot(direction, transform.right);
            Quaternion newRot = new Quaternion(axis.x, axis.y, axis.z, angle).normalized;
            Quaternion targetRotation = newRot * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.5f *Time.deltaTime);
           
            if (timer > cooldown)
            {
                Ray ray = new Ray(rightTarget.transform.position, rightTarget.transform.forward);
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        OnShootRight();
                    }
                }
                Ray ray2 = new Ray(leftTarget.transform.position, leftTarget.transform.forward);
                if (Physics.Raycast(ray2, out hit, 1000f))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        OnShootLeft();
                    }
                }
                timer = 0;
            }
        }

    }

    protected override void FreeSail()
    {
        if (currentDistance > detectionDistance)
        {

            currentSpeed += Time.deltaTime;
            var targetRotation = Quaternion.LookRotation(targetMove.position - transform.position);
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f * Time.deltaTime);
            rb.MovePosition(transform.position + transform.forward * currentSpeed * Time.deltaTime);
        }
    }
    

    protected override void OnShootRight()
    {
        GameObject ball = Instantiate(ammo, canons[0].transform.position, canons[0].transform.rotation);
        GameObject ball2 = Instantiate(ammo, canons[1].transform.position, canons[1].transform.rotation);
        sound.PlayOneShot(shootSound);
    }

    protected override void OnShootLeft()
    {
        GameObject ball = Instantiate(ammo, canons[2].transform.position, canons[2].transform.rotation);
        GameObject ball2 = Instantiate(ammo, canons[3].transform.position, canons[3].transform.rotation);
        sound.PlayOneShot(shootSound);
    }
}
