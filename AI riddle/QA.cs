using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;
namespace AI_riddle
{
   public class QA: TableEntity
    {
        
        public string Question { get; set; }
        public int id { get; set; }
        public string Answer { get; set; }
      



        public QA()
        {
            this.RowKey = Guid.NewGuid().ToString();
        }
    }
}
