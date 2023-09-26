using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.NoteLibrary
{
    public class SaveData: NoteList
    {
        public static void SaveToFile(string filepath)
        {
            try
            {
                using (StreamWriter fileWriter = File.CreateText(filepath))
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true // For pretty formatting in the JSON file
                    };

                    // Serialize the entire Notes collection
                    var jsonString = JsonSerializer.Serialize(Notes, options);
                    fileWriter.Write(jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving notes: " + ex.Message);
            }
        }
    }
}