using System;
using System.IO;

public class FileLogger : IDisposable
{
    private readonly string path;
    private FileStream fs;
    private StreamWriter sw;
    private bool enabled;
    private bool disposed;

    public FileLogger(string path)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            sw = new StreamWriter(fs, encoding: System.Text.Encoding.UTF8) { AutoFlush = true };
        }
        catch (Exception e) when (e is UnauthorizedAccessException || e is IOException)
        {
            MsgBox.ShowMessage("Warning", $"Failed to create log file: {e.Message}");
            enabled = false;
            return;
        }

        enabled = true;
        disposed = false;
        this.path = path;
    }

    public void LogInfo(string message)
    {
        if (!enabled || disposed) return;

        lock (sw)
        {
            sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [INFO] {message}");
        }
    }

    public void Dispose()
    {
        if (enabled && !disposed)
        {
            sw.Dispose();
            fs.Dispose();

            disposed = true;
        }
    }
}