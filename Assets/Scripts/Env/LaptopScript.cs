using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LaptopScript : MonoBehaviour, Interactable
{
    public string interactMessage = "Start coding";
    public bool isInteractable = true;
    public UIScript ui;
    
    string code = "";
    public string fileName = "Untitled";
    string compilerPath;

    string envPath;

    string result = "";

    string Interactable.interactMessage { get => interactMessage; }
    bool Interactable.isInteractable { get => isInteractable; }

    void Start()
    {
        envPath = Application.dataPath + "/Env/";
        makeCompilerPath();
    }

    public void interact()
    {
        if (!isInteractable) return;
        ui.showIDE(this);
    }

    public void highlight()
    {
        return;
    }

    public void unhighlight()
    {
        return;
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
        // verifica daca exista fisierul fileName.c in directorul Env/fileName
        // daca nu exista, returneaza string gol
        // altfel, returneaza continutul fisierului

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
        // verifica daca exista directorul Env/fileName
        // daca nu exista, creeaza-l

        // verifica daca exista fisierul fileName.c in directorul Env/fileName
        // daca nu exista, creeaza-l

        // scrie codul in fisier

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
        // verifica daca exista fisierul fileName.c in directorul Env/fileName
        // daca nu exista, afiseaza mesaj de eroare

        // compileaza codul cu output in compileMessage.txt
        // daca exista erori, afiseaza mesaj de eroare

        // daca nu exista erori, afiseaza mesaj de succes

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
        // verifica daca exista fisierul fileName.exe in directorul Env/fileName
        // daca nu exista, afiseaza mesaj 

        // ruleaza codul cu output in runMessage.txt

        // afiseaza continutul fisierului runMessage.txt

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

        result = output;
        return output;
    }

    public bool checkOutput(string output)
    {
        return string.Compare(result, output, StringComparison.InvariantCultureIgnoreCase) == 0;
    }
}
