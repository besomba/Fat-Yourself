using UnityEngine;
using System.Collections;

public class MainTextureManager : MonoBehaviour {
	
	public GUITexture texture;
	
	// Use this for initialization
	void Start () {
		texture = GetComponent<GUITexture>();
		
		 int textureHeight = texture.texture.height;
    int textureWidth = texture.texture.width;
    int screenHeight = Screen.height;
    int screenWidth = Screen.width;
 
    int screenAspectRatio = (screenWidth / screenHeight);
    int textureAspectRatio = (textureWidth / textureHeight);
 
    int scaledHeight;
    int scaledWidth;
    if (textureAspectRatio <= screenAspectRatio)
    {
        // The scaled size is based on the height
        scaledHeight = screenHeight;
        scaledWidth = (screenHeight * textureAspectRatio);
    }
    else
    {
        // The scaled size is based on the width
        scaledWidth = screenWidth;
        scaledHeight = (scaledWidth / textureAspectRatio);
    }
    float xPosition = screenWidth / 2 - (scaledWidth / 2);
    texture.pixelInset = 
        new Rect(xPosition, scaledHeight - scaledHeight, 
        scaledWidth, scaledWidth);
}
	
	// Update is called once per frame
	void Update () {
		texture = GetComponent<GUITexture>();
		
		 int textureHeight = texture.texture.height;
    int textureWidth = texture.texture.width;
    int screenHeight = Screen.height;
    int screenWidth = Screen.width;
 
    int screenAspectRatio = (screenWidth / screenHeight);
    int textureAspectRatio = (textureWidth / textureHeight);
 
    int scaledHeight;
    int scaledWidth;
        // The scaled size is based on the height
        scaledHeight = screenHeight;
        scaledWidth = (screenHeight * textureAspectRatio);
    //else
   // {
        // The scaled size is based on the width
        scaledWidth = screenWidth  + 50;
        scaledHeight = (scaledWidth / textureAspectRatio);
    //}
    float xPosition = screenWidth / 2 - (scaledWidth / 2);
    texture.pixelInset = 
        new Rect(xPosition, scaledHeight - scaledHeight, 
        scaledWidth, scaledWidth);
}
	}

