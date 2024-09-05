using System;
using System.IO;

public class FileLogger
{
    private string path;

    public FileLogger(string path_)
    {
        path = path_;
    }

    public void LogInfo(string message)
    {
        using (StreamWriter sw = new StreamWriter(path, append: true, encoding: System.Text.Encoding.UTF8))
        {
            sw.Write($"{DateTime.Now} [INFO] {message}\n");
        }
    }
}
