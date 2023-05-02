using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LaptopScript : MonoBehaviour
{
    public string interactMessage = "Press E to interact";
    
    string code = "";
    public string fileName = "Untitled";
    string compilerPath;

    string envPath;
    
    void Start()
    {
        envPath = Application.dataPath + "/Env/";
        makeCompilerPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: handle missing gcc from PATH
    void makeCompilerPath()
    {
        string systemEnvPath = System.Environment.GetEnvironmentVariable("PATH");
        string[] paths = systemEnvPath.Split(';');        
        compilerPath = paths.Select(x => Path.Combine(x, "gcc.exe"))
            .Where(x => File.Exists(x))
            .FirstOrDefault();
    }

    public void setCode(string code)
    {
        this.code = code;
    }

    public string getCode()
    {
        return code;
    }

    public void loadCode()
    {
        // check if file named fileName.c exists in Env/fileName directory
        // if not, return empty string
        // else, return the file content

        string path = envPath + fileName + "/" + fileName + ".c";
        if (!File.Exists(path))
        {
            code = "";
            Debug.Log("File does not exist. Path: " + path);
        }
        else
        {
            code = File.ReadAllText(path);
            Debug.Log("Code:\n" + code);
        }
    }

    public void saveCode()
    {
        // check if folder named fileName exists in Env directory
        // if not, create it

        // check if file named fileName.c exists in Env/fileName directory
        // if not, create it

        // write code to file

        if (!Directory.Exists(envPath + fileName))
        {
            Directory.CreateDirectory(envPath + fileName);
        }

        StreamWriter writer = new StreamWriter(envPath + fileName + "/" + fileName + ".c");
        code.Replace("\n", "\r\n");
        writer.Write(code);
        writer.Close();
    }

    public void compileCode()
    {
        // check if gcc exists in PATH
        // if not, show error message

        // check if file named fileName.c exists in Env/fileName directory
        // if not, show error message

        // compile code with output to compileMessage.txt
        // if error, show error message

        // if no error, show success message

        if (!File.Exists(envPath + fileName + "/" + fileName + ".c"))
        {
            GameObject.Find("Canvas").GetComponent<UIScript>().showMessage("File does not exist");
            // hide message after 1 second
            GameObject.Find("Canvas").GetComponent<UIScript>().Invoke("hideMessages", 1);
            return;
        }

        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.FileName = compilerPath;
        startInfo.Arguments = "\"" + envPath + fileName + "/" + fileName + ".c\" -o \"" + envPath + fileName + "/" + fileName + ".exe\"";
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;
        process.StartInfo = startInfo;
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            GameObject.Find("Canvas").GetComponent<UIScript>().showMessage("Compilation failed");
            // hide message after 1 second
            GameObject.Find("Canvas").GetComponent<UIScript>().Invoke("hideMessages", 1);

            StreamWriter writer = new StreamWriter(envPath + fileName + "/compileMessage.txt");
            writer.Write(output);
            writer.Close();
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<UIScript>().showMessage("Compilation successful");
            // hide message after 1 second
            GameObject.Find("Canvas").GetComponent<UIScript>().Invoke("hideMessages", 1);
        }

        process.Close();
        
    }

    public string runCode()
    {
        // check if file named fileName.exe exists in Env/fileName directory
        // if not, display "Compile first"

        // run code with output to runMessage.txt

        if (!File.Exists(envPath + fileName + "/" + fileName + ".exe"))
        {
            GameObject.Find("Canvas").GetComponent<UIScript>().showMessage("Compile first");
            // hide message after 1 second
            GameObject.Find("Canvas").GetComponent<UIScript>().Invoke("hideMessages", 1);
            return "";
        }
        
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

        startInfo.FileName = envPath + fileName + "/" + fileName + ".exe";
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;
        process.StartInfo = startInfo;
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        StreamWriter writer = new StreamWriter(envPath + fileName + "/runMessage.txt");
        writer.Write(output);
        writer.Close();

        return output;
    }
}
