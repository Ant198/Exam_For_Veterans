using System;

public struct ClothingProp
{
    public string typeClother { get; set; }
    public string name { get; set; }
    public string price { get; set; }
    public string color { get; set; }
    public int amount { get; set; }
    public string size { get; set; }
    public string sex { get; set; }
}

public class Clother {
    public ClothingProp clothingProp;
    
    public Clother( ClothingProp clothingProp )
    {
        this.clothingProp = clothingProp; 
    }
    
    
}