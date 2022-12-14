// 
//    Project:  handler.parse
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
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using ErrorOr;
global using FluentValidation;
global using MediatR;
global using Microsoft.Extensions.DependencyInjection;
global using Navient.Common.Extension;
global using Navient.Common.Model;
global using Newtonsoft.Json.Linq;
global using Serilog;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(@"Service.Handler.Parse.Test")]