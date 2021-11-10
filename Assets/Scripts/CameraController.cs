using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public float minZoom = 1;
    public float maxZoom = 30;
    public float currentOrtho;
    public float cameraMovSpeed;

    [Header("Camera Boundaries")] //kameran rajat x ja y akselilla. Molemmilla  akseleilla on - ja + sijainnit.
    public float xPlusBoundary = 46;
    public float xMinusBoundary = -46;
    public float yPlusBoundary = 46;
    public float yMinusBoundary = -46;


    public int boundary  = 10; // Etäisyys rajalta milloin liikkuvuus  alkaa

    private int theScreenWidth;
    private int theScreenHeight;


    void Start()
    {
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;
        if (maxZoom < 25)
            maxZoom = 25;
        cameraMovSpeed = maxZoom - 20;
    }

    void Update()
    {
        cameraMovSpeed = currentOrtho + 15;

        CameraZoom();
        CameraMove();
        CheckBoundaries();
    }


    void CameraMove()
    {
        //Liikuta kameraa nuolinäppäimillä tai WASD
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.Translate(new Vector3(cameraMovSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.Translate(new Vector3(-cameraMovSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.Translate(new Vector3(0, -cameraMovSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.Translate(new Vector3(0, cameraMovSpeed * Time.deltaTime, 0));
        }

        //Liikuta kameraa hiirellä reunaan
        if (Input.mousePosition.x > theScreenWidth - boundary)
        {
            Camera.main.transform.Translate(new Vector3(cameraMovSpeed * Time.deltaTime, 0, 0)); // move on +X axis
        }
        if (Input.mousePosition.x < 0 + boundary)
        {
            Camera.main.transform.Translate(new Vector3(-cameraMovSpeed * Time.deltaTime, 0, 0)); // move on -X axis
        }
        if (Input.mousePosition.y > theScreenHeight - boundary)
        {
            Camera.main.transform.Translate(new Vector3(0, cameraMovSpeed * Time.deltaTime, 0)); // move on +Y axis
        }
        if (Input.mousePosition.y < 0 + boundary)
        {
            Camera.main.transform.Translate(new Vector3(0, -cameraMovSpeed * Time.deltaTime, 0)); // move on -Y axis
        }
    }


    void CheckBoundaries()
    {
        theScreenWidth = Screen.width; //Katsotaan ruudun leveys
        theScreenHeight = Screen.height; //Katsotaan ruudun korkeus

        if (Camera.main.transform.position.x > xPlusBoundary) //Jos kameran x sijainti on enemmän kuin xPlusBoundary.
        {
            Camera.main.transform.position = new Vector3(xPlusBoundary, Camera.main.transform.position.y, Camera.main.transform.position.z); //kameran sijainti asetetaan rajalle
        }
        if (Camera.main.transform.position.x < xMinusBoundary) //Jos kameran x sijainti on vähemmän kuin xMinusBoundary.
        {
            Camera.main.transform.position = new Vector3(xMinusBoundary, Camera.main.transform.position.y, Camera.main.transform.position.z); //kameran sijainti asetetaan rajalle
        }
        if (Camera.main.transform.position.y > yPlusBoundary) //Jos kameran y sijainti on enemmän kuin yPlusBoundary.
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, yPlusBoundary, Camera.main.transform.position.z); //kameran sijainti asetetaan rajalle
        }
        if (Camera.main.transform.position.y < yMinusBoundary) //Jos kameran y sijainti on vähemmän kuin yMinusBoundary.
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, yMinusBoundary, Camera.main.transform.position.z); //kameran sijainti asetetaan rajalle
        }
    }

    void CameraZoom()
    {
        currentOrtho = Camera.main.orthographicSize; //Katsoo kameran nykyisen ortografisen koon.

        // Rullaa eteenpäin
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && minZoom != currentOrtho)
        {
            ZoomOrthoCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
        }

        // Rullaa taakse
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && maxZoom != currentOrtho)
        {
            ZoomOrthoCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition), -1);
        }
    }

    
    void ZoomOrthoCamera(Vector3 zoomTowards, float amount) // Ortografinen kamera zoomaa kohtaan (maailma koordinaateissa). Negatiivinen luku zoomaa takaisin, positiivinen zoomaa ulos
    {
        // Laske kuinka paljon meidän on siirryttävä  zoomTowards kohtaa kohti
        float multiplier = (1.0f / Camera.main.orthographicSize * amount);

        // Liikuta kameraa
        Camera.main.transform.position += (zoomTowards - Camera.main.transform.position) * multiplier;

        // Zoomaa kameraa
        Camera.main.orthographicSize -= amount;

        // Rajoita zoomia
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
    }

}



