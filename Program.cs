using System;
using System.IO;
using System.Text.Json;

class Program
{
      
   public static void addDataToJasonFile(string path, MyClothers[] addedClother)
      {  
         try
         {
            MyClothers[] oldClothers = readDataFromJsonFile(path);
            MyClothers[] newClothers = oldClothers.Concat(addedClother).ToArray();
            string clotherJson = JsonSerializer.Serialize(newClothers);
            File.WriteAllText(path, clotherJson);
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
         }
         finally
         {
            Console.WriteLine("Дані успішно додані");
         }
         
      }
   public static MyClothers[] readDataFromJsonFile(string path)
   {
      string clotherJson = File.ReadAllText(path);
      MyClothers[] clothers;
      if (clotherJson.Length == 0)
      {
         clothers = new MyClothers[0]; 
         return clothers;
      }
      else
      {
         clothers = JsonSerializer.Deserialize<MyClothers[]>(clotherJson);    
         return clothers;
      }
   }
   public static MyClothers[] addData()
   {
      MyClothers[] clother = new MyClothers[1];
      try
      {
         Console.WriteLine($"Введіть тип одягу (куртка, штани, шорти, футболка)");
         clother[0].typeClother = Console.ReadLine();
         if (clother[0].typeClother.Length == 0)
         {
            throw new ArgumentException("Не вірно введені дані");
         }
         Console.WriteLine($"Введіть назву");
         clother[0].name = Console.ReadLine();
         if (clother[0].name.Length == 0)
         {
            throw new ArgumentException("Не вірно введені дані");
         }
         Console.WriteLine($"Введіть ціну");
         clother[0].price = Console.ReadLine();
         if (clother[0].price.Length == 0)
         {
            throw new ArgumentException("Не вірно введені дані");
         }
         Console.WriteLine($"Введіть колір");
         clother[0].color = Console.ReadLine();
         if (clother[0].color.Length == 0)
         {
            throw new ArgumentException("Не вірно введені дані");
         }
         Console.WriteLine($"Введіть кількість");
         clother[0].amount = Console.ReadLine();
         if (clother[0].amount.Length == 0)
         {
            throw new ArgumentException("Не вірно введені дані");
         }
         Console.WriteLine($"Введіть розмір");
         clother[0].size = Console.ReadLine();
         if (clother[0].size.Length == 0)
         {
            throw new ArgumentException("Не вірно введені дані");
         }
         Console.WriteLine($"Введіть стать");
         clother[0].sex = Console.ReadLine();
         if (clother[0].sex.Length == 0)
         {
            throw new ArgumentException("Не вірно введені дані");
         }   
      }
      catch (ArgumentException e)
      {
         Console.WriteLine(e.Message);
         Environment.Exit(1);
      }
            
      return clother;
   }

   public static MyClothers[] getDataFromFile(string path)
   {
      string clotherFromFile = File.ReadAllText(path);;
      MyClothers[] clother = JsonSerializer.Deserialize<MyClothers[]>(clotherFromFile);
      try
      {
         if (clotherFromFile.Length == 0 || clother == null)
         {
            throw new ArgumentException("Файл не містить даних");
         }
      }
      catch (ArgumentException e)
      {
         Console.WriteLine(e.Message);
      }
      return clother;
   }

   private static void printData()
   {
      Console.WriteLine();
   }
   public static void Main(string[] args)
   {     
      string pathToJsonFile = "/home/ant/Ekreative course/basic_for_veteran/Exam_For_Veterans/data_Base/clothers.txt";
      string pathToFile = "/home/ant/Ekreative course/basic_for_veteran/Exam_For_Veterans/data_Base/clother.txt";
      int menuNumber = Convert.ToInt32(Console.ReadLine());
      
      switch (menuNumber)
      {
         case 1:
            printData();
            break;
         case 2:
            addDataToJasonFile(pathToJsonFile, addData());
            break;
          case 3:
         addDataToJasonFile(pathToJsonFile, getDataFromFile(pathToFile));
            ;
            break;
         /*case 4:
            ProcessingData();
            break;
         case 5:
            OperationData();
            break;
         case 6:
            break;
           */ 
         default:
            Console.WriteLine("Good Luck!");
            break;
      }      
   }    
}
