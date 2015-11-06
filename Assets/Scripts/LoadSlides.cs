using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
public class LoadSlides : MonoBehaviour
{

    public GameObject player;
    public GameObject mainCamera;
    private string[] textures;
    private int currentTexture;
    private Texture2D texture;
    // Use this for initialization
    void Start()
    {
        texture = new Texture2D(50, 50);
        var path = EditorUtility.OpenFolderPanel("Select the folder where the images are located", "", "");
        if (path.Length != 0)
        {
            
            textures = Directory.GetFiles(path, "*.jpg");
            
            if (textures.Length > 0)
            {
                currentTexture = 0;
                updateTexture();
            }
        }
    }

    private void updateTexture()
    {
       
        currentTexture = Mathf.Clamp(currentTexture, 0, textures.Length - 1);
        var www = new WWW("file:///" + textures[currentTexture]);
        www.LoadImageIntoTexture(texture);
        player.GetComponent<Renderer>().material.mainTexture = texture;
    }


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        mainCamera.transform.position += 0.5f*movement;

        if (Input.GetKey(KeyCode.G))
        {
            currentTexture--;
            updateTexture();
        }
        if (Input.GetKey(KeyCode.H))
        {
            currentTexture++;
            updateTexture();
        }

    }

}
