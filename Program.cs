using System;
using System.IO;
using System.Text.Json;

class Program
{
   private static void printData(string path)
   {
      MyClothers[] clothersArray = readDataFromJsonFile(path);
      foreach (MyClothers clother in clothersArray)
      {
         Console.WriteLine($"{clother.type}\t{clother.name}\t{clother.price}\t{clother.color}\t{clother.amount}\t{clother.size}\t{clother.sex}");
      }
   }
   public static void addDataToJasonFile(string path, MyClothers[] addedClother)
      {  
         MyClothers[] oldClothers = readDataFromJsonFile(path);
         MyClothers[] newClothers = oldClothers;
         try
         {
            
            for (int i = 0; i < oldClothers.Length; i++)
            {
               for (int j = 0; j < addedClother.Length; j++)
               {
                  if (
                        oldClothers[i].type.Equals(addedClother[j].type) && oldClothers[i].name.Equals(addedClother[j].name) &&
                        oldClothers[i].price.Equals(addedClother[j].price) && oldClothers[i].color.Equals(addedClother[j].color) &&
                        oldClothers[i].size.Equals(addedClother[j].size) && oldClothers[i].sex.Equals(addedClother[j].sex)
                     )
                  {
                     newClothers[i].amount++;
                  }
                  else
                  {
                     Array.Resize(ref newClothers, newClothers.Length + 1);
                     newClothers[newClothers.Length - 1] = addedClother[j];
                  }
               }
            }
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
   public static MyClothers[] getData()
   {
      MyClothers[] clother = new MyClothers[1];
      try
      {
         Console.WriteLine($"Введіть тип одягу (куртка, штани, шорти, футболка)");
         clother[0].type = Console.ReadLine();
         if (clother[0].type.Length == 0)
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
         clother[0].amount = int.Parse(Console.ReadLine());
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
      string clotherFromFile = File.ReadAllText(path);
      MyClothers[] clother = JsonSerializer.Deserialize<MyClothers[]>(clotherFromFile);
      try
      {
         if (clotherFromFile.Length == 0)
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

   public static void deleteClother(string path)
   {
      MyClothers[] interest = getData();
      MyClothers[] currentClothers = readDataFromJsonFile(path);
      MyClothers[] newClothers = currentClothers;
         try
         {
            
            for (int i = 0; i < currentClothers.Length; i++)
            {
               if (
                     currentClothers[i].type.Equals(interest[0].type) && currentClothers[i].name.Equals(interest[0].name) &&
                     currentClothers[i].price.Equals(interest[0].price) && currentClothers[i].color.Equals(interest[0].color) &&
                     currentClothers[i].size.Equals(interest[0].size) && currentClothers[i].sex.Equals(interest[0].sex)
                     )
                  {
                     newClothers[i].amount -= interest[0].amount;
                     if (newClothers[i].amount <= 0)
                     {
                        
                        Array.Resize(ref newClothers, newClothers.Length - 1);
                        
                     }
                  }
                  else
                  {
                     newClothers[newClothers.Length - 1] = addedClother[j];
                  }
   }
   public static void ProcessingData(string path)
   {
      int number = Convert.ToInt32(Console.ReadLine());
      switch (number)
      {
         case 1:
            deleteClother(path);
            break;
         default:
            break;
      }
      
   }
   public static void Main(string[] args)
   {     
      string pathToJsonFile = "/home/ant/Ekreative course/basic_for_veteran/Exam_For_Veterans/data_Base/clothers.txt";
      string pathToFile = "/home/ant/Ekreative course/basic_for_veteran/Exam_For_Veterans/data_Base/clother.txt";
      int menuNumber = Convert.ToInt32(Console.ReadLine());
      string greeting;
      Console.WriteLine("Виберіть, що вас цікаить");
      switch (menuNumber)
      {
         case 1:
            printData(pathToJsonFile);
            break;
         case 2:
            addDataToJasonFile(pathToJsonFile);
            break;
          case 3:
            addDataToJasonFile(pathToJsonFile, getDataFromFile(pathToFile));
            break;
         case 4:
            ProcessingData(pathToJsonFile);
            break;
         /*case 5:
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
