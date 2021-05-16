using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace TextBooker.DataAccess
{
    public class TextBookerContext : DbContext
    {
		public TextBookerContext(DbContextOptions<TextBookerContext> options) : base(options)
		{

		}
	}
}
