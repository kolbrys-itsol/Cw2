using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace APBDtest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var inputPath = args.Length > 0 ? args[0] : @"Files\dane.csv";
                var outputPath = args.Length > 1 ? args[1] : @"Files\Result.xml";
                var outputType = args.Length > 2 ? args[2] : "json";

                if (!File.Exists(inputPath))
                {
                    throw new FileNotFoundException("Nie znaleziono pliku: " + inputPath);
                }

                var university = new University();
                var uniqueCourses = new HashSet<string>();
                // var activeStudies = new ActiveStudies();

                foreach (var line in File.ReadAllLines(inputPath))
                {
                    var data = line.Split(",");
                    if (data.Length!=9)
                    {
                        File.AppendAllText("Log.txt", $"{DateTime.UtcNow} ERR Nieprawidłowa liczba wartości {line}" + "\n");
                        continue;

                    }

                    var valid = true;
                    for (var i = 0; i < data.Length; i++)
                    {
                        if (String.IsNullOrEmpty(data[i]))
                        {
                            valid = false;
                            File.AppendAllText("Log.txt", $"{DateTime.UtcNow} ERR brak części danych {line}" + "\n");
                            continue;
                        }
                    }
                    
                    var student = new Student
                    {
                        Name = data[0],
                        Surname = data[1],
                        IndexNum = "s"+data[4],
                        Date = data[5],
                        Mail = data[6],
                        MothersName = data[7],
                        FathersName = data[8],
                        Studies = new Studies{ studName = data[2],mode=data[3]}
                    };
                    if (valid)
                    {
                        university.Students.Add(student);
                        uniqueCourses.Add(data[2]);
                    }

                    
                }
                foreach(var courseName in uniqueCourses)
                {
                    var count = university.Students.Count((Student p) => p.Studies.studName.Equals(courseName));
                    if (count!=0)
                    {
                        university.ActiveStudies.Add(new Studies {studName = courseName, numberOfStudents = count.ToString()});
                    }
                }
                
                university.Author = "Robert Lewandowski";
                if (outputType.Equals("xml"))
                {
                    var serializer = new XmlSerializer(typeof(University));
                    var serializerSettings = new XmlWriterSettings();
                    var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                    serializerSettings.OmitXmlDeclaration = true;
                    serializerSettings.Indent = true;
                    var writer = XmlWriter.Create(new FileStream($"{outputPath}", FileMode.Create),serializerSettings);
                    serializer.Serialize(writer,university,emptyNamespaces);
                }else if (outputType.Equals("json"))
                {
                    if (args.Length < 2)
                    {
                        outputPath = @"Files/Result.json";
                    }

                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        IgnoreNullValues = true
                    }
                    ;
                    var serializer = JsonSerializer.Serialize(university,options);

                    File.WriteAllText(outputPath,serializer);
                }
                

            }
            catch (FileNotFoundException e)
            {
                File.AppendAllText("Log.txt",e.Message);
            }
            
        }
    }
}