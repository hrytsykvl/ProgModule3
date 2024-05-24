using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

/*
 Зчитує дані з файлу employees.xml. Файл містить список співробітників у форматі XML, де кожен співробітник має такі властивості: Name, Position, HireDate.
Сортує співробітників за датою прийому на роботу (від найстаріших до найновіших) за допомогою LINQ.
Зберігає відсортований список співробітників у новий XML файл sorted_employees.xml.
Записує інформацію про співробітників в текстовий файл employees.txt у наступному форматі:

Name: [Name] Position: [Position] HireDate: [HireDate]
*/

public class Employee
{
	public string Name { get; set; }
	public string Position { get; set; }
	public DateTime HireDate { get; set; }

	public override string ToString()
	{
		return $"Name: {Name}, Position: {Position}, HireDate: {HireDate}";
	}
}

public class Program
{
	public static void Main()
	{
		string inputPath = "E:\\Projects C-type lang\\C# Projects\\ProgModule3\\ProgModule3\\employees.xml";
		string filteredPath = "E:\\Projects C-type lang\\C# Projects\\ProgModule3\\ProgModule3\\sorted_employees.xml";
		string outputPathTxt = "E:\\Projects C-type lang\\C# Projects\\ProgModule3\\ProgModule3\\employees.txt";

		List<Employee> employees = DeserializeEmployees(inputPath);
        Console.WriteLine("Deserialized employees: ");
        employees.ForEach(Console.WriteLine);
        Console.WriteLine("\n=================================\n");

        var filteredEmployees = employees.OrderBy(employees => employees.HireDate);
		filteredEmployees.ToList().ForEach(Console.WriteLine);
		Console.WriteLine("\n=================================\n");

		SerializeEmployees(filteredEmployees.ToList(), filteredPath);
        Console.WriteLine("Successfully serialized into xml file");
		Console.WriteLine("\n=================================\n");

		using(StreamWriter streamWriter = new StreamWriter(outputPathTxt))
		{
			foreach (var employee in employees)
			{
				streamWriter.WriteLine($"Name: [{employee.Name}] Position: [{employee.Position}] HireDate: [{employee.HireDate:yyyy-MM-dd}]");
			}
		}
		Console.WriteLine("Successfully written into txt file");
		Console.WriteLine("\n=================================\n");
	}

	private static List<Employee> DeserializeEmployees(string filePath)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>), new XmlRootAttribute("Employees"));
		using (FileStream fs = new FileStream(filePath, FileMode.Open))
		{
			return (List<Employee>)serializer.Deserialize(fs);
		}
	}

	private static void SerializeEmployees(List<Employee> employees, string filePath)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>), new XmlRootAttribute("Employees"));
		using (StreamWriter writer = new StreamWriter(filePath))
		{
			serializer.Serialize(writer, employees);
		}
	}
}