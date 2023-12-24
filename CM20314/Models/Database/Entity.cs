using System;
using System.ComponentModel.DataAnnotations;
namespace CM20314.Models.Database
{
	public abstract class Entity
	{
		[Key]
		public int Id { get; set; }
	}
}

