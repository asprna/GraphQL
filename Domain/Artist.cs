﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class Artist
	{
		public int ArtistId { get; set; }
		public string Name { get; set; }
		public ICollection<Album> Albums { get; set; } = new List<Album>();
	}
}