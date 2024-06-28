using AutoMapper;

using ShareIt.Core.Application;
using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public class CommentServices : GenericServices<Comment, CommentSaveViewModel, CommentViewModel>, ICommentServices
    {
        public readonly ICommentRepository _repository;

        public readonly IMapper _mapper;

        public CommentServices(ICommentRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }


        public override async Task<CommentSaveViewModel> AddSaveViewModel(CommentSaveViewModel vm)
        {
            Comment comment = _mapper.Map<Comment>(vm);

            comment.DateTime = DateTime.Now;

            if (comment.IdParentComment != null ) {

                Comment parentComment = await GetByIdAsync((int)vm.IdParentComment);

                if (parentComment.Replies == null)
                {
                    parentComment.Replies = new List<Comment>();
                }

                comment.IdPublication = parentComment.IdPublication;

                parentComment.Replies.Add(comment);


               await UpdateAsync(parentComment, parentComment.Id);
            
            
            }else
            {
                await AddAsync(comment);
                
            }

            CommentSaveViewModel Vm = _mapper.Map<CommentSaveViewModel>(comment);

            return Vm;
        }

       
    }
  
}
