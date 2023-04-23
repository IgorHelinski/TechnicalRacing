using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MachineGunner : MonoBehaviour
{
    [Header("Setup")]
    public MyInput inputMenager;
    public CameraFollow cameraScript;
    public Camera mainCamera;
    private Rigidbody carRb;

    [Header("Gun Setup")]
    public GameObject Browning;
    public Transform barrelEnd;
    public GameObject projectile;
    public GameObject impact;
    public AudioSource shootSound;
    public TextMeshProUGUI ammoUI;
    public Image crosshair;

    [Header("GunStats")]
    public float fireRate;
    public int ammo;
    bool readyToFire;

    Vector2 turn;

    void Start()
    {
        //mainCamera = Camera.main;
        carRb = this.GetComponent<Rigidbody>();

        readyToFire = true;
        Cursor.visible = false;
        crosshair.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        ammoUI.text = ammo.ToString();

        Vector2 m = new Vector2(inputMenager.Xon, inputMenager.Yon);
        
        if (inputMenager.scopeInput)
        {
            Cursor.visible = false;
            crosshair.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            
            cameraScript.relativePosition = CameraFollow.RelativePosition.gunnerPos;
            RaycastHit hit;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0f);

            crosshair.transform.position = Input.mousePosition;

            if (inputMenager.controllerInput)
            {
                turn.x = Input.GetAxis("Joy X");
                turn.y = Input.GetAxis("Joy Y");
                Vector2 joystick = new Vector2(turn.x, turn.y);

                var nwm = mainCamera.ViewportToScreenPoint(joystick);
                Vector3 finalVector = new Vector3(nwm.x + (Screen.width / 2), nwm.y + (Screen.height / 2), 0f);
                inputMenager.mouse.WarpCursorPosition(finalVector);
            }

            if (Physics.Raycast(mainCamera.ViewportPointToRay(mousePosition), out hit, 1000))
            {
                Debug.DrawRay(mainCamera.transform.position, hit.point - Browning.transform.position);
                
                Vector3 lookDirection = hit.point - Browning.transform.position;
                Quaternion lookRot = Quaternion.LookRotation(lookDirection);
                Browning.transform.rotation = Quaternion.Slerp(Browning.transform.rotation, lookRot, 3 * Time.deltaTime);

                if (inputMenager.shootInput && readyToFire && ammo >= 1)
                {
                    StartCoroutine(Shoot());
                }
            }
            else
            {
                Browning.transform.localRotation = Quaternion.Slerp(Browning.transform.localRotation, Quaternion.Euler(0, -90, -90), 3 * Time.fixedDeltaTime);
            }
        }
        else
        {
            Cursor.visible = false;
            crosshair.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            cameraScript.relativePosition = CameraFollow.RelativePosition.InitalPosition;
            Browning.transform.localRotation = Quaternion.Slerp(Browning.transform.localRotation, Quaternion.Euler(0, -90, -90), 5 * Time.fixedDeltaTime);
        }
    }

    IEnumerator Shoot()
    {
        readyToFire = false;
        yield return new WaitForSeconds(fireRate);
        carRb.AddForce(-Browning.transform.up * 5f, ForceMode.Impulse);
        GameObject projectileObject = Instantiate(projectile, barrelEnd.position, Browning.transform.rotation);
        projectileObject.GetComponent<Projectile>().Shoot();
        ammo--;
        shootSound.Play();
        readyToFire = true;
    }
}
