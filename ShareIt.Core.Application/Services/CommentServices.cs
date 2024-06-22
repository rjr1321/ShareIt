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
    }
  
}
