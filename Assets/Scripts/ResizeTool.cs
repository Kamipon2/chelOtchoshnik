using System;
using UnityEngine;
using UnityEngine.UI;
public class ResizeTool
{


 public static void Resize(Texture2D texture2D, int targetX, int targetY, bool mipmap =true, FilterMode filter = FilterMode.Bilinear)
  {
    //create a temporary RenderTexture with the target size
    RenderTexture rt = RenderTexture.GetTemporary(targetX, targetY, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);

    //set the active RenderTexture to the temporary texture so we can read from it
    RenderTexture.active = rt;
    
    //Copy the texture data on the GPU - this is where the magic happens [(;]
    Graphics.Blit(texture2D, rt);
    //resize the texture to the target values (this sets the pixel data as undefined)
    texture2D.Reinitialize(targetX, targetY, texture2D.format, mipmap);
    texture2D.filterMode = filter;

    try
    {
      //reads the pixel values from the temporary RenderTexture onto the resized texture
      texture2D.ReadPixels(new Rect(0.0f, 0.0f, targetX, targetY), 0, 0);
      //actually upload the changed pixels to the graphics card
      texture2D.Apply();
    }
    catch
    {
      Debug.LogError("Read/Write is not enabled on texture "+ texture2D.name);
    }


    RenderTexture.ReleaseTemporary(rt);
  }
}

