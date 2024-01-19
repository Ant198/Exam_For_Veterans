using System;
using System.IO;
using System.Text.Json;

class Program
{
   public struct MyClothers
   {
      public string typeClother 
      {
         get {return this.typeClother; } 
         set
         {
            if (value.Length == 0)
            {
               Console.WriteLine("не введені дані");
               return;
            }
            this.typeClother = value;
         }
      }
      public string name 
       {
         get {return this.name; } 
         set
         {
            if (value.Length == 0)
            {
               Console.WriteLine("не введені дані");
               return;
            }
            this.name = value;
         }
      }
      public string price 
       {
         get {return this.price; } 
         set
         {
            if (value.Length == 0)
            {
               Console.WriteLine("не введені дані");
               return;
            }
            this.price = value;
         }
      }
      public string color 
       {
         get {return this.color; } 
         set
         {
            if (value.Length == 0)
            {
               Console.WriteLine("не введені дані");
               return;
            }
            this.color = value;
         }
      }
      public string amount
      {
         get {return this.amount; } 
         set
         {
            if (value.Length == 0)
            {
               Console.WriteLine("не введені дані");
               return;
            }
            this.amount = value;
         }
      }
      public string size
      {
         get {return this.size; } 
         set
         {
            if (value.Length == 0)
            {
               Console.WriteLine("не введені дані");
               return;
            }
            this.size = value;
         }
      }
      public string sex
      {
         get {return this.sex; } 
         set
         {
            if (value.Length == 0)
            {
               Console.WriteLine("не введені дані");
               return;
            }
            this.sex = value;
         }
      }
   }   
   public static void addDataToJasonFile(string path, MyClothers addedClother)
      {  
         try
         {
            MyClothers[] oldClothers = readDataFromJsonFile(path);
            MyClothers[] lastestClother = { addedClother };
            MyClothers[] newClothers = oldClothers.Concat(lastestClother).ToArray();
            string clotherJson = JsonSerializer.Serialize(newClothers);
            File.WriteAllText(path, clotherJson);
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
         }
         finally
         {
            Console.WriteLine("finish");
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
   public static MyClothers addData(MyClothers clother)
   {
     
      Console.WriteLine($"Введіть тип одягу (куртка, штани, шорти, футболка)");
      clother.typeClother = Console.ReadLine();
      Console.WriteLine($"Введіть назву");
      clother.name = Console.ReadLine();
      Console.WriteLine($"Введіть ціну");
      clother.price = Console.ReadLine();
      Console.WriteLine($"Введіть колір");
      clother.color = Console.ReadLine();
      Console.WriteLine($"Введіть кількість");
      clother.amount = Console.ReadLine();
      Console.WriteLine($"Введіть розмір");
      clother.size = Console.ReadLine();
      Console.WriteLine($"Введіть стать");
      clother.sex = Console.ReadLine();
      
      return clother;
   }

   /*public static Clother getDataFromFile(string path)
   {
      string clotherFromFile = File.ReadAllText(path);
      Clother clother = JsonSerializer.Deserialize<Clother>(clotherFromFile);
      Clother[]
      return clother;
   }*/

   private static void printData()
   {
      Console.WriteLine();
   }
   public static void Main(string[] args)
   {     
      string pathToJsonFile = "/home/ant/Ekreative course/basic_for_veteran/Exam_For_Veterans/data_Base/clothers.txt";
      string pathToFile = "/home/ant/Ekreative course/basic_for_veteran/Exam_For_Veterans/data_Base/clother.txt";
      int menuNumber = Convert.ToInt32(Console.ReadLine());
      MyClothers clother = new MyClothers();
      switch (menuNumber)
      {
         case 1:
            printData();
            break;
         case 2:
            addDataToJasonFile(pathToJsonFile, addData(clother));
            break;
        /* case 3:
         addDataToJasonFile(pathToJsonFile, getDataFromFile(pathToFile));
            ;
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
