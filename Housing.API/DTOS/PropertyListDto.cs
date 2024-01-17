using Housing.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Housing.API.DTOS
{
    public class PropertyListDto
    {
        public int Id { get; set; }
        public int sellRent { get; set; }
        public string Name { get; set; }
        public string PropertyType { get; set; }
        public int BHK { get; set; }
       
        public string FurnishingType { get; set; }
        public int Price { get; set; }
        public int BuiltArea { get; set; }
       // public int CarpetArea { get; set; }
        //public string Address { get; set; }
        //public string Address2 { get; set; }
       
        public string City { get; set; }
       // public int FloorNo { get; set; }
       // public int TotalFloors { get; set; }

        public bool ReadyToMove { get; set; }
      //  public string MainEntrance { get; set; }
      //  public int Security { get; set; }
     //   public bool Gated { get; set; }
      //  public int Maintenance { get; set; }
        
        public DateTime EstPossissionOn { get; set; }
        public string Country { get; set; }
        public string photo { get; set; }



    }
}
