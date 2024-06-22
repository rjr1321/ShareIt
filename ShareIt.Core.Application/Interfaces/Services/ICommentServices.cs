using ShareIt.Core.Application;
using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public interface ICommentServices : IGenericServices<Comment, CommentSaveViewModel, CommentViewModel>
    {
    }
}
