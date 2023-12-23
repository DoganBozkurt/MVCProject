﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EntityLayer.Concrete
{
	public class SplineChartData
	{
		public string? day;
		public int income;
		public int expense;
        public int amount;
    }
}