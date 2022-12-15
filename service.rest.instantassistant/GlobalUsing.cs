// 
//    Project:  rest.instantassistant
//    Created:  08-2022-11
// 
//    COPYRIGHT © 2022 - 2030 Navient Solutions, LLC. All rights reserved
// 
//    COPYRIGHT NOTICE
//    The entire contents of this file and the files that make up the assembly that this file resides in are the express property of Navient Solutions, LLC. All Rights Reserved.
//    None of the contents of this file or assembly may be copied or duplicated in whole or part by any means without express prior agreement in writing or unless
//    specifically noted within this file or copyright notice of this file, assembly, or API.
// 
//    Some of the content contained within this file, assembly or API may be the copyrighted property of others; acknowledgement of those copyrights is hereby given.
//    All such material is used with the proper license or permission of the owner.
// 

global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Linq;
global using System.Net.Http;
global using System.Text;
global using System.Text.Json;
global using System.Text.RegularExpressions;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Authentication.Negotiate;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Navient.Presentation.Service.Rest.Entities;
global using Navient.Presentation.Service.Rest.Helpers;
global using Navient.Presentation.Service.Rest.Services;
global using Serilog;