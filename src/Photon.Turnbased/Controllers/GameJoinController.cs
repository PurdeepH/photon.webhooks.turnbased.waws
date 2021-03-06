﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameJoinController.cs" company="Exit Games GmbH">
//   Copyright (c) Exit Games GmbH.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Photon.Webhooks.Turnbased.Controllers
{
    using Models;
    using Newtonsoft.Json;


    public class GameJoinController : Controller
    {
        private readonly ILogger<GameJoinController> _logger;

        public GameJoinController(ILogger<GameJoinController> logger)
        {
            _logger = logger;
        }

        #region Public Methods and Operators

        [HttpPost]
        public IActionResult Index([FromBody] GameLeaveRequest request, [FromHeader] string appId)
        {
            var response = new OkResponse();
            _logger.LogInformation($"{Request.GetUri()} - {JsonConvert.SerializeObject(response)}");
            return Ok(response);
        }

        #endregion
    }
}