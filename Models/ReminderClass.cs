using System.ComponentModel.DataAnnotations;

namespace demo5.Models
{
    public class ReminderClass
    {

        public string modstudy { get; set; }


        [DataType(DataType.Date)]
        public DateTime datestudy { get; set; }
       
    }
}
