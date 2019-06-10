using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Domain.Contracts.ViewModels;

namespace Course.ITnews.Domain.Contracts
{
    public interface ICommentaryService
    {
        IEnumerable<CommentaryViewModel> GetAll();
        CommentaryViewModel Get(int id);
        void Add(CommentaryViewModel viewModel);
        void Delete(int id);
    }
}
