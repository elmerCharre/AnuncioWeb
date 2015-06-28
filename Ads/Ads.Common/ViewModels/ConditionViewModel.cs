using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class ConditionViewModel
    {
        public ConditionViewModel()
        {
        }

        public ConditionViewModel(conditions condition)
        {
            this.Id = condition.Id;
            this.Name = condition.name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
