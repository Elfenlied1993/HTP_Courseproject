using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Domain.Contracts.ViewModels;

namespace Course.ITnews.Domain.Contracts
{
    public interface ICommentaryService
    {
        CommentaryViewModel Get(string id);
        void Add(CommentaryViewModel viewModel);
        void Edit(CommentaryViewModel viewModel);
        void Delete(string id);
    }
}
