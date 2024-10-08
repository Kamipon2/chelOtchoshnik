using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;
using System;
using System.ComponentModel;

public class StrimerWriter : MonoBehaviour
{
    // Start is called before the first frame update
    public string path;
    public Light[] light;
    [TextArea]
    public string text;
    public int i;
    public void Start()
    {
        Stats();
    }
    public async Task Stats()
    {
        if(!File.Exists(path))
        {
        using (StreamWriter writer = new StreamWriter(path, false))
        {
           await writer.WriteLineAsync(text);
        } 
        }
        using (StreamReader sr = new StreamReader(path))
            {
                while(0 == 0)
                {
                    i++;
                    //Debug.Log(sr.ReadLine() + i);
                    //Debug.Log(sr.Peek());
                    //string txt = sr.ReadLine().Substring(sr.ReadLine().IndexOf('=') + 1);
                    if(i == 2)
                    {
                        //light[0].shadows = LightShadows.sr.ReadLine().Substring(sr.ReadLine().IndexOf('=') + 1);
                        //light[0].shadows = Convert.ChangeType(sr.ReadLine().Substring(sr.ReadLine().IndexOf('=') + 1), LightShadows);
                        //if(txt == "0")
                        //{
                           // light[0].shadows = LightShadows.None;
                        //}
                    }
                }
            }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
