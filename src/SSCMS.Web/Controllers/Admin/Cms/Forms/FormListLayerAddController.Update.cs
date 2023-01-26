﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Core.Utils;
using SSCMS.Dto;

namespace SSCMS.Web.Controllers.Admin.Cms.Forms
{
    public partial class FormListLayerAddController
    {
        [HttpPost, Route(RouteUpdate)]
        public async Task<ActionResult<BoolResult>> Update([FromBody] UpdateRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                MenuUtils.SitePermissions.FormList))
            {
                return Unauthorized();
            }

            var form = await _formRepository.GetAsync(request.SiteId, request.FormId);
            form.Title = request.Title;
            form.Description = request.Description;

            await _formRepository.UpdateAsync(form);

            return new BoolResult
            {
                Value = true
            };
        }
    }
}
