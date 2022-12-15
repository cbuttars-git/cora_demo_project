// 
//    Project:  rest.instantassistant
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

using Navient.Presentation.Service.Rest.Web.Models.Feedback;

namespace Navient.Presentation.Service.Rest.Services;

/// <summary>
/// </summary>
public interface IFeedbackService
{
  /// <summary>
  /// </summary>
  /// <returns></returns>
  IEnumerable<Feedback> GetAll();

  /// <summary>
  /// </summary>
  /// <param name="sessionId"></param>
  /// <returns></returns>
  IEnumerable<Feedback> GetAllBySession(Guid sessionId);

  /// <summary>
  /// </summary>
  /// <param name="feedback"></param>
  /// <returns></returns>
  int Create(FeedbackRequest feedback);
}

/// <summary>
/// </summary>
public class FeedbackService : IFeedbackService
{
  private readonly DataContext _context;
  private readonly Regex _htmlTags = new(@"<[^>]*(>|$)");
  private readonly Regex _unicode = new(@"[^\u0000-\u007F]");


  /// <summary>
  /// </summary>
  /// <param name="context"></param>
  public FeedbackService(DataContext context)
  {
    _context = context;
  }

  /// <summary>
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  public int Create(FeedbackRequest model)
  {
    if (model.FeedbackId > -1)
    {
      var feedback = _context.Feedback?.SingleOrDefault(f => f.ID == model.FeedbackId);
      if (feedback == null) return model.FeedbackId;
      feedback.isSuccess = model.Success;
      _context.Feedback.Update(feedback);
      _context.SaveChanges();

      return model.FeedbackId;
    }
    else
    {
      var feedback = new Feedback
      {
        SessionID = model.SessionId,
        FeedbackLog = model.Response,
        FeedbackLogRpt = _htmlTags.Replace(_unicode.Replace(model.Response, string.Empty), string.Empty),
        isSuccess = model.Success
      };

      _context.Feedback?.Add(feedback);
      _context.SaveChanges();
      return feedback.ID;
    }
  }

  /// <summary>
  /// </summary>
  /// <returns></returns>
  public IEnumerable<Feedback> GetAll()
  {
    return _context.Feedback?.Select(f => new Feedback
    {
      ID = f.ID,
      SessionID = f.SessionID,
      FeedbackLog = f.FeedbackLog,
      isSuccess = f.isSuccess
    }).ToList();
  }

  /// <summary>
  /// </summary>
  /// <param name="sessionId"></param>
  /// <returns></returns>
  public IEnumerable<Feedback> GetAllBySession(Guid sessionId)
  {
    return _context.Feedback?.Where(f => f.SessionID == sessionId)
        .Select(f => new Feedback
        {
          ID = f.ID,
          SessionID = f.SessionID,
          FeedbackLog = f.FeedbackLog,
          isSuccess = f.isSuccess
        })
        .ToList();
  }
}