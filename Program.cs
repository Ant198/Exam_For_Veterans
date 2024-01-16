using System;
using System.IO;
using System.Text.Json;

class Program
{
   
/*
   private static void OperationData()
   {
      throw new NotImplementedException();
   }

   private static void ProcessingData()
   {
      throw new NotImplementedException();
   }

   private static void addDataFromFile()
   {
      throw new NotImplementedException();
   }
*/
   public static void addDataToJasonFile()
      {
         string clotherJson = JsonSerializer.Serialize( addData());
         Console.WriteLine($"after - {clotherJson}");
         File.WriteAllText("/home/ant/Ekreative course/basic_for_veteran/Exam_For_Veterans/data_Base/jacket.txt", clotherJson);
      }
   public static Clother[] addData()
   {
      
      Console.WriteLine($"Введіть тип одягу (куртка, штани, шорти, футболка)");
      string typeClother = Console.ReadLine();
      Console.WriteLine($"Введіть назву");
      string name = Console.ReadLine();
      Console.WriteLine($"Введіть ціну");
      string price = Console.ReadLine();
      Console.WriteLine($"Введіть колір");
      string color = Console.ReadLine();
      Console.WriteLine($"Введіть кількість");
      string amount = Console.ReadLine();
      Console.WriteLine($"Введіть розмір");
      string size = Console.ReadLine();
      Console.WriteLine($"Введіть стать");
      string sex = Console.ReadLine();

      Clother[] clothers = {new Clother( typeClother, name, price, color, amount, size, sex)};
      
      return clothers;
   }

   private static void printData()
   {
      Console.WriteLine();
   }
   public static void Main(string[] args)
   {     
      int menuNumber = Convert.ToInt32(Console.ReadLine());
      switch (menuNumber)
      {
         case 1:
            printData();
            break;
         case 2:
            addDataToJasonFile();
            break;
         /*case 3:
            addDataFromFile();
            break;
         case 4:
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
