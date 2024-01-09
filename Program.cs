using System;

class Program
{
   public struct Clother 
   {
      public string typeClother { get; set; }
      public string name { get; set; }
      public string article { get; set; }
      public string price { get; set; }
      public string color { get; set; }
      public string amount { get; set; }
      public string size { get; set; }
      public string sex { get; set; }

   }
   public static void Main()
   {
        int menuNumber = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(menuNumber);
        switch (menuNumber)
        {
            case 1:
               printData();
               break;
            case 2:
               addData();
               break;
            case 3:
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
            
            default:
               Console.WriteLine("Good Luck!");
               break;
        }
   }

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

    private static void addData()
    {
        throw new NotImplementedException();
    }

    private static void printData()
    {
        throw new NotImplementedException();
    }
}
