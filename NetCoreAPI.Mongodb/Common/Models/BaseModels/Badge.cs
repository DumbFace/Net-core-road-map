using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.BaseModels
{
    public class Badge
    {
        public int Id { get; set; }

        public int UserId { get; set; }
    }
}
