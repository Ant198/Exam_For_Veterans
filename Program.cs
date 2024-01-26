using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

class Program
{
   public static MyClothers[]bufferMemory = new MyClothers[0];
   public static void getMenu()
   {
      string pathToJsonFile = "/home/ant/Ekreative course/basic_for_veteran/Exam_For_Veterans/data_Base/clothers.txt";
      string pathToFile = "/home/ant/Ekreative course/basic_for_veteran/Exam_For_Veterans/data_Base/clother.txt";
      MyClothers[] clothersMas = readDataFromJsonFile(pathToJsonFile);
      Console.WriteLine("Виберіть, що вас цікаить:");
      Console.WriteLine("1 - Вивести дані на екран   2 - Ввести дані   3 - Додати дані з файлу   4 - Обробка даних ");
      Console.WriteLine("5 - Операці з даними   6 - Зберегти дані   7 - Вийти з програми");
      int menuNumber = Convert.ToInt32(Console.ReadLine());
      switch (menuNumber)
      {         
         case 1:
            printData(clothersMas);
            getMenu();
            break;   
         case 2:
            addDataToJasonFile(pathToJsonFile, getData());
            break;         
         case 3:
            addDataToJasonFile(pathToJsonFile, getDataFromFile(pathToFile));
            break;
           
         case 4:
            editingData(pathToJsonFile);
            break;         
         case 5:
            operationData(clothersMas);
            break;
         case 6:
            saveData(pathToJsonFile, pathToFile);
            break;
         default:
            Console.WriteLine("Good Luck!");
            break;
      }      
   }
   private static void printData(MyClothers[] clothersMas)
   {
      Console.WriteLine("type\tname\tprice\tcolor\tamount\tsize\tsex");
      Console.WriteLine();
      foreach (MyClothers clother in clothersMas)
      {
         Console.WriteLine($"{clother.type}\t{clother.name}\t{clother.price}\t{clother.color}\t{clother.amount}\t{clother.size}\t{clother.sex}");
      }
   }
   
   public static void addDataToJasonFile(string path, MyClothers[] addedClother)
   {  
      MyClothers[] oldClothers = readDataFromJsonFile(path);
      MyClothers[] newClothers = oldClothers;
      bool isExist = true;
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
                  isExist = true;
                  break;
               }
               else
               {
                 isExist = false;
               }
            }
            if (isExist == false)
            {
               Array.Resize(ref newClothers, newClothers.Length + 1);
               newClothers[newClothers.Length - 1] = addedClother[i];  
            }             
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
      Console.WriteLine("1 - Вивести одяг за статтю\t2 - Вивести одяг по типу\t3 - Вивести одяг за розміром\t4 - Вивести одяг за кольором\t5 - за зростанням ціни\t6 - за спаданням\t7 - скинути налаштування\t8 - вийти");
      int menuNumber = Convert.ToInt32(Console.ReadLine());
      getClothers(clothersMas, menuNumber);
      Console.WriteLine();
      printData(bufferMemory);
      Console.WriteLine();
      operationData(clothersMas);
   }

   public static void getClothers(MyClothers[] clothers, int number)
   {
      if (bufferMemory.Length == 0)
      {
         bufferMemory = clothers;
      }
      MyClothers[] filteredClother = new MyClothers[0];
      string filterStr;
      if (number == 1)
      {
         Console.WriteLine("введіть стать");
         filterStr = Console.ReadLine();
         if (filterStr.Length == 0)
         {
            Console.WriteLine("Невірно введені дані");
            getMenu();
         }
         for (int i = 0; i < bufferMemory.Length; i++)
         {
            if (bufferMemory[i].sex.Equals(filterStr))
            {
               Array.Resize(ref filteredClother, filteredClother.Length + 1);
               filteredClother[filteredClother.Length - 1] = bufferMemory[i];
            }
         }
         if(filteredClother.Length == 0)
         {
            Console.WriteLine("Шуканого одягу не існує");
            bufferMemory = new MyClothers[0];
            getMenu();
         }
         else
         {
             bufferMemory = filteredClother;
         }
      }
      if (number == 2)
      {
         Console.WriteLine("введіть тип одягу");
         filterStr = Console.ReadLine();
         if (filterStr.Length == 0)
         {
            Console.WriteLine("Невірно введені дані");
            getMenu();
         }
         for (int i = 0; i < bufferMemory.Length; i++)
         {
            if (bufferMemory[i].type.Equals(filterStr))
            {
               Array.Resize(ref filteredClother, filteredClother.Length + 1);
               filteredClother[filteredClother.Length - 1] = bufferMemory[i];
            }
         }
         if(filteredClother.Length == 0)
         {
            Console.WriteLine("Шуканого одягу не існує");
            bufferMemory = new MyClothers[0];
            getMenu();
         }
         else
         {
            bufferMemory = filteredClother;
         }
      }
      if (number == 3)
      {
         Console.WriteLine("введіть розмір");
         filterStr = Console.ReadLine();
         if (filterStr.Length == 0)
         {
            Console.WriteLine("Невірно введені дані");
            getMenu();
         }
         for (int i = 0; i < bufferMemory.Length; i++)
         {
            if (bufferMemory[i].size.Equals(filterStr))
            {
               Array.Resize(ref filteredClother, filteredClother.Length + 1);
               filteredClother[filteredClother.Length - 1] = bufferMemory[i];
            }
         }
         if(filteredClother.Length == 0)
         {
            Console.WriteLine("Шуканого одягу не існує");
            bufferMemory = new MyClothers[0];
            getMenu();
         }
         else
         {
            bufferMemory = filteredClother;
         }
      }
      if (number == 4)
      {
         Console.WriteLine("введіть колір");
         filterStr = Console.ReadLine();
         if (filterStr.Length == 0)
         {
            Console.WriteLine("Невірно введені дані");
            getMenu();
         }
         for (int i = 0; i < bufferMemory.Length; i++)
         {
            if (bufferMemory[i].color.Equals(filterStr))
            {
               Array.Resize(ref filteredClother, filteredClother.Length + 1);
               filteredClother[filteredClother.Length - 1] = bufferMemory[i];
            }
         }
         if(filteredClother.Length == 0)
         {
            Console.WriteLine("Шуканого одягу не існує");
            bufferMemory = new MyClothers[0];
            getMenu();
         }
         else
         {
            bufferMemory = filteredClother;
         }
      }
      if (number == 5)
      {
         getUpPriceSort(clothers);
      }
      if (number == 6)
      {
         getDownPriceSort(clothers);
      }
      if (number == 7)
      {
         bufferMemory = new MyClothers[0];
      }
      if (number == 8)
      {
         getMenu();
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
   
   public static void getUpPriceSort(MyClothers[] clothers)
   {
      if (clothers.Length == 0)
      {
         Console.WriteLine("Дані відсутні");
         getMenu();
      }       
      if (bufferMemory.Length == 0)
      {
         bufferMemory = clothers;
      }
      for (int i = 0; i < bufferMemory.Length - 1; i++)
      {
         bool isSorted = true;
         for (int j = 1; j < bufferMemory.Length - i; j++)
         {
            if (bufferMemory[j].price < bufferMemory[j - 1].price)
            {
               (bufferMemory[j], bufferMemory[j - 1]) = (bufferMemory[j - 1], bufferMemory[j]);
               isSorted = false;
            }
         }
         if (isSorted)
         {
            break;
         }
      }
   }
   public static void getDownPriceSort(MyClothers[] clothers)
   {
      if (clothers.Length == 0)
      {
         Console.WriteLine("Дані відсутні");
         getMenu();
      }       
      if (bufferMemory.Length == 0)
      {
         bufferMemory = clothers;
      }
      for (int i = 0; i < bufferMemory.Length - 1; i++)
      {
         bool isSorted = true;
         for (int j = 1; j < bufferMemory.Length - i; j++)
         {
            if (bufferMemory[j].price > bufferMemory[j - 1].price)
            {
               (bufferMemory[j], bufferMemory[j - 1]) = (bufferMemory[j - 1], bufferMemory[j]);
               isSorted = false;
            }
         }
         if (isSorted)
         {
            break;
         }
      }
   }
   
   public static void saveData(string path,string pathToFile)
   {
      Console.WriteLine("Введіть число:");
      Console.WriteLine("1 - якщо хочете додати з клавіатури   2 - якщо хочете додати з файлу");
      int number = Convert.ToInt32(Console.ReadLine());
      if (number == 1)
      {
         addDataToJasonFile(path, getData());
      }
      if (number == 2)
      {
         addDataToJasonFile(path, getDataFromFile(pathToFile));
      }
      
   }
   public static void Main(string[] args)
   {  
      getMenu();
   }    
}