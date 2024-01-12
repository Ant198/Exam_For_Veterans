using System;
using System.Collections.Generic;

class Program
{
   public static List<Clother> clothers = new List<Clother>();

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
   private static void addData( List<Clother> listClothers)
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
      int amount = Convert.ToInt32(Console.ReadLine());
      Console.WriteLine($"Введіть розмір");
      string size = Console.ReadLine();
      Console.WriteLine($"Введіть стать");
      string sex = Console.ReadLine();
      
      listClothers.Add(new Clother(new ClothingProp{ typeClother = typeClother, name = name, price = price,
                                                     color = color, amount = amount,
                                                     size = size, sex = sex}));      
   }

   private static void printData(List<Clother> clothers)
   {
      Console.WriteLine(clothers[0]);
   }
   public static void Main(string[] args)
   {     
      Console.WriteLine($"before - {clothers.Count}");
      int menuNumber = Convert.ToInt32(Console.ReadLine());
      switch (menuNumber)
      {
         case 1:
            printData(clothers);
            break;
         case 2:
            addData(clothers);
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
      for (int i = 0; i < clothers.Count; i++)
      {
         
            Console.WriteLine(clothers[i].clothingProp.name);
            
         
      }
      Console.WriteLine($"after - {clothers.Count}");
   }    
}
