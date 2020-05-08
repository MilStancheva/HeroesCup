/*
 * Copyright (c) 2019 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Piranha.Manager;

namespace ClubsModule.Areas.ClubsModule.Pages
{
    [Authorize(Policy = Permission.PagesEdit)]
    public class ClubsEditViewModel : PageModel
    {
    }
}