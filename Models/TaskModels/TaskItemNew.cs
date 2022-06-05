using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProgrammingCompetitionService.Models
{
    public class TaskItemNew: ITaskItem
    {        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

