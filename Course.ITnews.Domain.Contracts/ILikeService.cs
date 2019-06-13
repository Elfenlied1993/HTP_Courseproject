using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Domain.Contracts.ViewModels;

namespace Course.ITnews.Domain.Contracts
{
    public interface ILikeService
    {
        IEnumerable<LikeViewModel> GetAll(int commentId);

        LikeViewModel Get(int id);
        void Add(LikeViewModel viewModel);
        void Delete(int id);
    }
}
