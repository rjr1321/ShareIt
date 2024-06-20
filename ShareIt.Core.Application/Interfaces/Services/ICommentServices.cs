using ShareIt.Core.Application.ViewModels.Comment;
using ShareIt.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application.Interfaces.Services
{
    public interface ICommentServices : IGenericServices<Comment, CommentSaveViewModel, CommentViewModel>
    {
    }
}
