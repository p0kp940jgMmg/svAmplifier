using Microsoft.AspNetCore.Mvc.Rendering;
using svAmplifier.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.VM
{
	public class MyItemsVM
	{
		public string Message { get; set; } = "Couldnt add ur pick";
		public Pick NewPick { get; set; }
        public SelectListItem[] Mushrooms { get; set; }
        public SelectListItem[] Regions { get; set; }
        public int SelectedMushID { get; set; }

    }
}
