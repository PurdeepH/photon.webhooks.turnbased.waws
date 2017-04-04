﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameLeaveController.cs" company="Exit Games GmbH">
//   Copyright (c) Exit Games GmbH.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Photon.Turnbased;
using Photon.Turnbased.DataAccess;

namespace Photon.Webhooks.Turnbased.Controllers
{
    using System.Web.Http;
    using Models;
    using Newtonsoft.Json;

    public class GameLeaveController : Controller
    {
        private readonly ILogger<GameLeaveController> _logger;

        #region Public Methods and Operators
        public GameLeaveController(ILogger<GameLeaveController> logger)
        {
            _logger = logger;
        }
        public dynamic Post(GameLeaveRequest request, string appId)
        {
            string message;
            if (!IsValid(request, out message))
            {
                var errorResponse = new ErrorResponse { Message = message };
                _logger.LogError($"{Request.GetUri()} - {JsonConvert.SerializeObject(errorResponse)}");
                return errorResponse;
            }

            if (request.IsInactive)
            {
                if (request.ActorNr > 0)
                {
                    DataSources.DataAccess.GameInsert(appId, request.UserId, request.GameId, request.ActorNr);
                }
            }
            else
            {
                DataSources.DataAccess.GameDelete(appId, request.UserId, request.GameId);
            }

            var okResponse = new OkResponse();
            _logger.LogInformation($"{Request.GetUri()} - {JsonConvert.SerializeObject(okResponse)}");
            return okResponse;
        }

        private static bool IsValid(GameLeaveRequest request, out string message)
        {
            if (string.IsNullOrEmpty(request.GameId))
            {
                message = "Missing GameId.";
                return false;
            }

            if (string.IsNullOrEmpty(request.UserId))
            {
                message = "Missing UserId.";
                return false;
            }

            message = "";
            return true;
        }

        #endregion
    }
}