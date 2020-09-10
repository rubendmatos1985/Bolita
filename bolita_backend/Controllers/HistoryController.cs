using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace bolita_backend.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HistoryController : ControllerBase
	{

		private readonly ILogger<HistoryController> _logger;

		public HistoryController(ILogger<HistoryController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Get()
		{
			
			return Ok(new {/* elements = dailyResults*/ });
		}
	}


}
