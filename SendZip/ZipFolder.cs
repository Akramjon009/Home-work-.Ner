
using System.IO.Compression;

public  class Zips
{

    public  static string Zipfiles(string filename)
    {

        //string filename2 = SearchDirections(@"C:\", filename);

        string startPath = $"{filename}";
        string zipPath = $"{filename}.zip";
        ZipFile.CreateFromDirectory(startPath, zipPath);
        return zipPath;
    }

    public static string SearchDirections(string folderPath, string mainFile)
    {

        string[] directions = Directory.GetDirectories(folderPath, "*", SearchOption.TopDirectoryOnly);

        foreach (string subdirectory in directions)
        {
            try
            {

                if (subdirectory.Contains(mainFile))
                {
                    return subdirectory.ToString();
                }
                SearchDirections(subdirectory, mainFile);
            }
            catch { }
        }
        return "";
    }
}
