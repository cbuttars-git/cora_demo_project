// 
//    Project:  rest.mockfiservproxy
//    Created:  07-2022-11
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
global using System.IO;
global using System.Linq;
global using System.Reflection;
global using System.Threading.Tasks;
global using DotNetEnv;
global using MediatR;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Hosting.Server.Features;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ApiExplorer;
global using Microsoft.AspNetCore.Mvc.Versioning;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Navient.Common.Extension;
global using Navient.Presentation.Configuration.Extension;
global using Navient.Presentation.Configuration.Model;
global using Navient.Presentation.Service.Handler;
global using Navient.Presentation.Service.Rest.Configuration;
global using Newtonsoft.Json;
global using Serilog;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using static Navient.Presentation.Configuration.Extension.SerilogStartupExtension;
global using static Navient.Presentation.Service.Rest.Constant;
global using ILogger = Serilog.ILogger;