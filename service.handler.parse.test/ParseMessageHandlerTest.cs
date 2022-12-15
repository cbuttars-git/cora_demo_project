// 
//    Project:  handler.parse.test
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

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Navient.Common.Model;
using Newtonsoft.Json;

namespace Navient.Test.Presentation.Service.Handler;

/// <summary>
/// </summary>
public class ParseMessageHandlerTest
{
  private const string _CLASS_NAME = nameof(ParseMessageHandlerTest);


  /// <summary>
  /// </summary>
  public static Root RootFactory => new()
  {
      Expense = 748625.369d,
      Name = @"Some Test Name",
      FirstChild = new FirstChild
      {
          Address = @"9999 First Street",
          ContactName = @"Some Contact I Know",
          Contacts = new List<SecondChild>
          {
              new()
              {
                  Address = @"123 Another rd",
                  ContactName = @"I Forgot Who This is",
                  DateAdded = new DateTime(1976, 6, 1, 22, 36, 11),
                  PhoneNumber = @"(800) 123-4567",
                  State = @"MT"
              },
              new()
              {
                  Address = @"456 I am lost rd",
                  ContactName = @"Kings",
                  DateAdded = new DateTime(1978, 10, 15, 10, 12, 35),
                  PhoneNumber = @"(801) 123-4569",
                  State = @"Colorado"
              },
              new()
              {
                  Address = @"888 Where Am I",
                  ContactName = @"Goose",
                  DateAdded = new DateTime(1993, 2, 25, 13, 41, 58),
                  PhoneNumber = @"(801) 555-4569",
                  State = @"UT"
              }
          },
          Count = 15,
          DateAdded = new DateTime(2015, 4, 15, 17, 14, 48),
          DueDates = new List<DateTime>
          {
              DateTime.Now.AddYears(-50).AddDays(25).AddMonths(-6),
              DateTime.Now.AddYears(-10).AddDays(5).AddMonths(-5),
              DateTime.Now.AddYears(-21).AddDays(8).AddMonths(3)
          },
          PhoneNumber = @"(866) 357-9876",
          State = @"California",
          ScienceCalculation = 145672364297.9871671F * 1000 / 99,
          SecondChild = new SecondChild
          {
              Address = @"1 Between Lost & Found",
              ContactName = @"Goose",
              DateAdded = new DateTime(1993, 2, 25, 13, 41, 58),
              PhoneNumber = @"(801) 555-4569",
              State = @"ID"
          }
      }
  };

  /// <summary>
  /// </summary>
  [Fact(DisplayName = $@"{_CLASS_NAME} Class: {nameof(ParseMessageHandlerReturnsFormattedMessageTest)} Returns Correct Message Test")]
  public async Task ParseMessageHandlerReturnsFormattedMessageTest()
  {
    var jsonObject = RootFactory;
    var expectedMessage = $@"My First Address is {jsonObject.FirstChild.Address}.";
    var jsonData = jsonObject.ToJson();
    var messageRequest = new MessageRequest(new Request { Data = jsonData, Message = @"My First Address is ~first_child.address~." });

    var actualMessage = await new ParseMessageHandler().Handle(messageRequest, CancellationToken.None).ConfigureAwait(false);
    Assert.NotNull(actualMessage);
    Assert.Equal(expectedMessage, actualMessage);

    expectedMessage =
        $@"I have several contacts, in fact I have {jsonObject.FirstChild.Count}. The second contact in my list is {jsonObject.FirstChild.Contacts[1].ContactName}. He lives at {jsonObject.FirstChild.Contacts[1].Address} {jsonObject.FirstChild.Contacts[1].State}. I added this contact on {jsonObject.FirstChild.Contacts[1].DateAdded:D}";
    messageRequest = new MessageRequest(new Request
    {
        Data = jsonData,
        Message =
            @"I have several contacts, in fact I have ~first_child.count~. The second contact in my list is ~first_child.contact_list[1].contact_name~. He lives at ~first_child.contact_list[1].address~ ~first_child.contact_list[1].state~. I added this contact on ~first_child.contact_list[1].date_added~"
    });

    actualMessage = await new ParseMessageHandler().Handle(messageRequest, CancellationToken.None).ConfigureAwait(false);
    Assert.NotNull(actualMessage);
    Assert.Equal(expectedMessage, actualMessage);
  }
}

/// <summary>
/// </summary>
public class Root : JsonBase<Root>
{
  /// <summary>
  /// </summary>
  public double Expense { get; set; }

  /// <summary>
  /// </summary>
  public FirstChild FirstChild { get; set; }

  /// <summary>
  /// </summary>
  public string Name { get; set; }
}

/// <summary>
/// </summary>
public class FirstChild : SecondChild
{
  /// <summary>
  /// </summary>
  public float ScienceCalculation { get; set; }

  /// <summary>
  /// </summary>
  public int Count { get; set; }

  /// <summary>
  /// </summary>
  public List<DateTime> DueDates { get; set; }

  /// <summary>
  /// </summary>
  [JsonProperty(@"contact_list")]
  public List<SecondChild> Contacts { get; set; }

  /// <summary>
  /// </summary>
  [JsonProperty(@"contact")]
  public SecondChild SecondChild { get; set; }
}

/// <summary>
/// </summary>
public class SecondChild
{
  /// <summary>
  /// </summary>
  public DateTime DateAdded { get; set; }

  /// <summary>
  /// </summary>

  public string Address { get; set; }

  /// <summary>
  /// </summary>

  public string ContactName { get; set; }

  /// <summary>
  /// </summary>

  public string PhoneNumber { get; set; }

  /// <summary>
  /// </summary>

  public string State { get; set; }
}