﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Core.Repositories;
using SSCMS.Core.Utils;

namespace SSCMS.Web.Controllers.Admin.Cms.Forms
{
    public partial class FormListController
    {
        [HttpPost, Route(RouteDelete)]
        public async Task<ActionResult<DeleteResult>> Delete([FromBody] DeleteRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId,
                MenuUtils.SitePermissions.FormList))
            {
                return Unauthorized();
            }

            var form = await _formRepository.GetAsync(request.SiteId, request.FormId);
            var relatedIdentities = _formRepository.GetRelatedIdentities(form.Id);

            await _tableStyleRepository.DeleteAllAsync(FormDataRepository.TABLE_NAME, relatedIdentities);
            await _formDataRepository.DeleteByFormIdAsync(form.Id);
            await _formRepository.DeleteAsync(request.SiteId, form.Id);

            var forms = await _formRepository.GetFormsAsync(request.SiteId);

            return new DeleteResult
            {
                Forms = forms
            };
        }
    }
}
