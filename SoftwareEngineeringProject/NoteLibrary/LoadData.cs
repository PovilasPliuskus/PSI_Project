using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
namespace SoftwareEngineeringProject.NoteLibrary
{
    public class LoadData: NoteList
    {
        public static void LoadFromFile(string filePath)
        {
            try
            {
                using (StreamReader fileReader = File.OpenText(filePath))
                {
                    var jsonString = fileReader.ReadToEnd();

                    // Deserialize the entire Notes collection
                    Notes = JsonSerializer.Deserialize<List<Note>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading notes: " + ex.Message);
            }
        }
    }
}