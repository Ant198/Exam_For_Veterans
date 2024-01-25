using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

class Program
{
   public static MyClothers[]bufferMemory;
   public static void getMenu()
   {
      string pathToJsonFile = "/home/ant/Ekreative course/basic_for_veteran/Exam_For_Veterans/data_Base/clothers.txt";
      string pathToFile = "/home/ant/Ekreative course/basic_for_veteran/Exam_For_Veterans/data_Base/clother.txt";
      MyClothers[] clothersMas = readDataFromJsonFile(pathToJsonFile);
      Console.WriteLine("Виберіть, що вас цікаить");
      int menuNumber = Convert.ToInt32(Console.ReadLine());
      switch (menuNumber)
      {
         
         case 1:
            printData(clothersMas);
            getMenu();
            break;
         /*   
         case 2:
            addDataToJasonFile(pathToJsonFile, getData());
            break;
         
         case 3:
            addDataToJasonFile(pathToJsonFile, getDataFromFile(pathToFile));
            break;
           
         case 4:
            editingData(pathToJsonFile);
            break;
         */
         case 5:
            operationData(clothersMas);
            break;
         /*   
         case 6:
            break;
         */ 
         default:
            Console.WriteLine("Good Luck!");
            break;
      }      
   }
   private static void printData(MyClothers[] clothersMas)
   {
      foreach (MyClothers clother in clothersMas)
      {
         Console.WriteLine($"{clother.type}\t{clother.name}\t{clother.price}\t{clother.color}\t{clother.amount}\t{clother.size}\t{clother.sex}");
      }
   }
   
   public static void addDataToJasonFile(string path, MyClothers[] addedClother)
   {  
      MyClothers[] oldClothers = readDataFromJsonFile(path);
      MyClothers[] newClothers = oldClothers;
      string clotherJson ;
      if (oldClothers.Length == 0)
      {
         clotherJson = JsonSerializer.Serialize(addedClother);
         File.WriteAllText(path, clotherJson);
      }
      else
      {
         for (int i = 0; i < addedClother.Length; i++)
         {
            for (int j = 0; j < oldClothers.Length; j++)
            {
               if (
                  oldClothers[j].type.Equals(addedClother[i].type) && oldClothers[j].name.Equals(addedClother[i].name) &&
                  oldClothers[j].color.Equals(addedClother[i].color) && oldClothers[j].size.Equals(addedClother[i].size) && 
                  oldClothers[j].sex.Equals(addedClother[i].sex)
                  )
               {
                  newClothers[j].price = addedClother[i].price;
                  newClothers[j].amount += addedClother[i].amount;
                  break;
               }
            }
            Array.Resize(ref newClothers, newClothers.Length + 1);
            newClothers[newClothers.Length - 1] = addedClother[i];
         }
            clotherJson = JsonSerializer.Serialize(newClothers);
            File.WriteAllText(path, clotherJson);
      }
       Console.WriteLine();
      getMenu();
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
         clother[0].price = int.Parse(Console.ReadLine());
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
      MyClothers[] wantedClother = getData();
      MyClothers[] clothersMas = readDataFromJsonFile(path);
      MyClothers[] newClothersMas = clothersMas;
      bool isExist = true;
         try
         {
            for (int i = 0; i < clothersMas.Length; i++)
            {
               if (
                  clothersMas[i].type.Equals(wantedClother[0].type) && clothersMas[i].name.Equals(wantedClother[0].name) &&
                  clothersMas[i].color.Equals(wantedClother[0].color) && clothersMas[i].size.Equals(wantedClother[0].size) &&
                  clothersMas[i].sex.Equals(wantedClother[0].sex)
                  )
               {
                  isExist = true;
                  newClothersMas[i].amount -= wantedClother[0].amount;
                  if (newClothersMas[i].amount <= 0)
                  {
                     (newClothersMas[i], newClothersMas[newClothersMas.Length - 1]) = (newClothersMas[newClothersMas.Length - 1], newClothersMas[i]);
                     Array.Resize(ref newClothersMas, newClothersMas.Length - 1);
                  }
                  break;
               }
               else
               {
                  isExist = false;
               }
            }
            if (isExist == false)
            {
               throw new ArgumentException("Такої моделі одягу не знайдено");
            }
         }
         catch(ArgumentException e)
         {
            Console.WriteLine(e.Message);
         }
      string clotherJson = JsonSerializer.Serialize(newClothersMas);
      File.WriteAllText(path, clotherJson);    
   
   }


   public static void editPrice(string path)
   {
      MyClothers[] wantedClother = getData();
      MyClothers[] clothersMas = readDataFromJsonFile(path);
      bool isExist = true;
      try
      {
         for (int i = 0; i < clothersMas.Length; i++)
         {
            if (
               clothersMas[i].type.Equals(wantedClother[0].type) && clothersMas[i].name.Equals(wantedClother[0].name) &&
               clothersMas[i].color.Equals(wantedClother[0].color) && clothersMas[i].size.Equals(wantedClother[0].size) &&
               clothersMas[i].sex.Equals(wantedClother[0].sex)
               )
            {
               isExist = true;
               clothersMas[i].price = wantedClother[0].price;
               break;
            }   
            else
            {
               isExist = false;
            }
         }
         if (isExist == false)
         {
            throw new ArgumentException("model doesn't exist");
         }   
      }
      catch (ArgumentException e)
      {
         Console.WriteLine(e.Message);
      }
      string clotherJson = JsonSerializer.Serialize(clothersMas);
      File.WriteAllText(path, clotherJson);    
   }

   public static void editingData(string path)
   {
      Console.WriteLine("1 - Якщо хочете видалити модель\t2 - Якщо хочете додати модель\t3 - Змінити ціну моделі\t4 - Повернутися до основного меню");
      int number = Convert.ToInt32(Console.ReadLine());
      switch (number)
      {
         case 1:
            deleteClother(path);
            break;
            
         case 2:
            addDataToJasonFile(path, getData());
            break;
         case 3:
            editPrice(path);
            break;
      
         default:
            getMenu();
            break;
      }
      Console.WriteLine();
      getMenu();
   }

   public static void operationData(MyClothers[] clothersMas)
   {
      
   
      Console.WriteLine("Виберіть пункт меню");
      Console.WriteLine("1 - Вивести одяг по типу і по статті\t2 - Вивести одяг по статі, типу, кольору\t3 - Сортувати за зростанням ціни\t4 - Сортування за спаданням ціни\t5 - скинути");
      int menuNumber = Convert.ToInt32(Console.ReadLine());
      
      switch (menuNumber)
      {
         case 1:
            getClothesByTypeSex(clothersMas);
            printData(bufferMemory);
            operationData(clothersMas);
            break;
         case 2:
            getClothesByTypeSexColor(clothersMas);
            printData(bufferMemory);
            operationData(clothersMas);
            break;
         case 3:
            upPriceSorting(bufferMemory);
            printData(bufferMemory);
         case 5:
         System.Console.WriteLine(bufferMemory[0].name);
            bufferMemory = new MyClothers[0];
            System.Console.WriteLine(bufferMemory);
            getMenu();
            break;
         default:
            getMenu();
            break;
      }      
   }

   public static void getClothesByTypeSex(MyClothers[] clothers)
   {
      MyClothers[] getClothers = new MyClothers[0];
      try
      {
         Console.WriteLine("введіть тип одягу");
         string type = Console.ReadLine();
         Console.WriteLine("введіть стать");
         string sex = Console.ReadLine();
         if (type.Length == 0 || sex.Length == 0)
         {
            throw new ArgumentException("Невірно введені дані");
         }
         for (int i = 0; i < clothers.Length; i++)
         {
            if (clothers[i].type.Equals(type) && clothers[i].sex.Equals(sex))
            {
               Array.Resize(ref getClothers, getClothers.Length + 1);
               getClothers[getClothers.Length - 1] = clothers[i];
            }
         }
         if(getClothers.Length == 0)
         {
            throw new ArgumentException("Шуканого одягу не існує");
         }
      }
      catch (ArgumentException e)
      {
         Console.WriteLine(e.Message);
      }
      bufferMemory = getClothers;
   }

   public static void getClothesByTypeSexColor(MyClothers[] clothers)
   {
       MyClothers[] getClothers = new MyClothers[0];
      try
      {
         Console.WriteLine("введіть тип одягу");
         string type = Console.ReadLine();
         Console.WriteLine("введіть стать");
         string sex = Console.ReadLine();
         Console.WriteLine("введіть колір");
         string color = Console.ReadLine();
         if (type.Length == 0 || sex.Length == 0 || color.Length == 0)
         {
            throw new ArgumentException("Невірно введені дані");
         }
         for (int i = 0; i < clothers.Length; i++)
         {
            if (clothers[i].type.Equals(type) && clothers[i].sex.Equals(sex) && clothers[i].color.Equals(color))
            {
               Array.Resize(ref getClothers, getClothers.Length + 1);
               getClothers[getClothers.Length - 1] = clothers[i];
            }
         }
         if(getClothers.Length == 0)
         {
            throw new ArgumentException("Шуканого одягу не існує");
         }
      }
      catch (ArgumentException e)
      {
         Console.WriteLine(e.Message);
      }
      bufferMemory = getClothers;
   }
   
   public static void upPriceSorting(MyClothers[] clothers)
   {
      if (clothers.Length == 0)
      {
         Console.WriteLine("Невірно введені дані");
         getMenu();
      }
        
      for (int i = 0; i < clothersMas.Length - 1; i++)
        {
            bool isSorted = true;
            for (int j = 1; j < clothersMas.Length - i; j++)
            {
                if (clothersMas[j] < mas[j - 1])
                {
                    int tmp = mas[j];
                    mas[j] = mas[j - 1];
                    mas[j - 1] = tmp;
                    isSorted = false;
                }
            }
            if (isSorted)
            {
                break;
            }
//            PrintArray(mas);
        }
   
   }
   public static void Main(string[] args)
   {  
      getMenu();upPriceSorting(bufferMemory);
      Console.WriteLine(bufferMemory);
   }    
}