using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VehicleGun : MonoBehaviour
{
    [Header("Setup")]
    public VehicleInput inputMenager;
    public VehicleCamera cameraScript;
    public Camera mainCamera;
    private Rigidbody carRb;

    [Header("Gun Setup")]
    public Transform GunRotatePoint;
    public Transform GunBarrelEnd;
    public Vector3 gunRestEuler; // the rotation of the gun while resting

    [Header("VFX")]
    public GameObject projectile;
    public GameObject impact;
    public AudioSource shootSound;
    public TextMeshProUGUI ammoUI;
    public Image crosshair;

    [Header("GunStats")]
    public float fireRate;
    public int ammo;
    bool readyToFire;

    void Start()
    {
        carRb = this.GetComponent<Rigidbody>();

        readyToFire = true;

        Cursor.visible = false;
        crosshair.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        ammoUI.text = ammo.ToString();

        if (inputMenager.scopeInput)
        {
            // show crosshair
            Cursor.visible = false;
            crosshair.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;

            RaycastHit hit;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0f);

            // bind crosshair to the mouse pos
            crosshair.transform.position = Input.mousePosition;

            if (Physics.Raycast(mainCamera.ViewportPointToRay(mousePosition), out hit, 1000))
            {
                // if ray hit something
                Debug.DrawRay(mainCamera.transform.position, hit.point - GunRotatePoint.position);

                Vector3 lookDirection = hit.point - GunRotatePoint.position;
                Quaternion lookRot = Quaternion.LookRotation(lookDirection);
                GunRotatePoint.rotation = Quaternion.Slerp(GunRotatePoint.rotation, lookRot, 3 * Time.deltaTime);

                if (inputMenager.shootInput && readyToFire && ammo >= 1)
                {
                    StartCoroutine(Shoot());
                }
            }
            else
            {
                // if didn't hit anything
                // go back to rest pos
                GunRotatePoint.localRotation = Quaternion.Slerp(GunRotatePoint.localRotation, Quaternion.Euler(gunRestEuler), 3 * Time.fixedDeltaTime);
            }
        }
        else
        {
            // no input
            Cursor.visible = false;
            crosshair.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;

            // go to rest pos
            GunRotatePoint.localRotation = Quaternion.Slerp(GunRotatePoint.localRotation, Quaternion.Euler(gunRestEuler), 5 * Time.fixedDeltaTime);
        }
    }

    IEnumerator Shoot()
    {
        readyToFire = false;
        yield return new WaitForSeconds(fireRate);
        carRb.AddForce(-GunRotatePoint.up * 5f, ForceMode.Impulse);
        GameObject projectileObject = Instantiate(projectile, GunBarrelEnd.position, GunBarrelEnd.rotation);
        projectileObject.GetComponent<Projectile>().Shoot();
        ammo--;
        shootSound.Play();
        readyToFire = true;
    }
}
