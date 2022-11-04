namespace InstantAssistant.Api.Services;

using System.Text.RegularExpressions;
using InstantAssistant.Api.Entities;
using InstantAssistant.Api.Helpers;
using InstantAssistant.Api.Web.Models.Feedback;

public interface IFeedbackService
{
  IEnumerable<Feedback> GetAll();
  IEnumerable<Feedback> GetAllBySession(Guid sessionId);
  int Create(FeedbackRequest feedback);
}

public class FeedbackService : IFeedbackService
{
  private DataContext _context;

  private readonly Regex htmlTags = new Regex(@"<[^>]*(>|$)");
  private readonly Regex unicode = new Regex(@"[^\u0000-\u007F]");

  public FeedbackService(DataContext context)
  {
    _context = context;
  }

  public IEnumerable<Feedback> GetAll()
  {
    return _context.Feedback.Select(f => new Feedback{
      ID = f.ID,
      SessionID = f.SessionID,
      FeedbackLog = f.FeedbackLog,
      isSuccess = f.isSuccess
    }).ToList<Feedback>();
  }

  public IEnumerable<Feedback> GetAllBySession(Guid sessionId)
  {
    return _context.Feedback.Select(f => new Feedback{
      ID = f.ID,
      SessionID = f.SessionID,
      FeedbackLog = f.FeedbackLog,
      isSuccess = f.isSuccess
    })
    .Where(f => f.SessionID == sessionId)
    .ToList<Feedback>();
  }

  public int Create(FeedbackRequest model)
  {
    if (model.FeedbackId > -1)
    {
      var feedback = _context.Feedback.SingleOrDefault(f => f.ID == model.FeedbackId);
      if (feedback != null)
      {
        feedback.isSuccess = model.Success;
        _context.Feedback.Update(feedback);
        _context.SaveChanges();
      }
      return model.FeedbackId;
    }
    else
    {
      var feedback = new Feedback
      {
        SessionID = model.SessionId,
        FeedbackLog = model.Response,
        FeedbackLogRpt = htmlTags.Replace(unicode.Replace(model.Response, String.Empty), String.Empty),
        isSuccess = model.Success
      };

      _context.Feedback.Add(feedback);
      _context.SaveChanges();
      return feedback.ID;
    }
  }
}
